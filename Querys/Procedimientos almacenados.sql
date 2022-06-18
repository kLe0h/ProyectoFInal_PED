use ProyectoPED_FaseFinal;
go



select * from producto;
go

-- PROCEDIMIENTOS ALMACENADOS USUARIOS
create proc sp_RegistrarUsuario(
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
AS
begin
	SET @Resultado = 0

	IF NOT EXISTS (SELECT * FROM usuario WHERE Correo = @Correo)
	begin 
		insert into usuario(Nombres, Apellidos, Correo, Clave, Activo) values
		(@Nombres, @Apellidos, @Correo, @Clave, @Activo)

		SET @Resultado = SCOPE_IDENTITY()
	end

	ELSE
		set @Mensaje = 'El correo del usuario ya existe'
end
go

create proc sp_EditarUsuario(
@IdUsuario int,
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Activo bit,
@Mensaje varchar(500) output, -- Saber si hay error al final
@Resultado int output -- Operacion ha sido un exito (si/no)

)
as 
begin
	SET @Resultado = 0

	IF NOT EXISTS (SELECT * FROM usuario WHERE Correo = @Correo and IdUsuario != @IdUsuario)
	begin 
		UPDATE top (1) usuario set
		Nombres = @Nombres,
		Apellidos = @Apellidos,
		Correo = @Correo,
		Activo = @Activo
		WHERE IdUsuario = @IdUsuario

		SET @Resultado = 1
	end 

	ELSE
		set @Mensaje = 'El correo del usuario ya existe'
end

go

-- PROCEDIMIENTOS ALMACENADOS CATEGORIA
create proc sp_RegistrarCategoria(
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado int output
)
AS
begin 
	SET @Resultado = 0
	IF NOT EXISTS (select * from categoria where Descripcion = @Descripcion)
	begin 
		insert into categoria(Descripcion, Activo) values (@Descripcion, @Activo)
		set @Resultado = SCOPE_IDENTITY()
	end
	else 
		set @Mensaje = 'La categoria ya existe'
end

go

create proc sp_EditarCategoria(
@IdCategoria int,
@Descripcion varchar(100),
@Activo bit,
@Mensaje varchar(500) output,
@Resultado bit output
)
as
begin 
	set @Resultado = 0
	IF NOT EXISTS (select * from categoria where Descripcion = @Descripcion and IdCategoria != @IdCategoria)
	begin
		update top (1) categoria set
		Descripcion = @Descripcion,
		Activo = @Activo
		where IdCategoria = @IdCategoria

		set @Resultado = 1
	end
	else
		set @Mensaje = 'La categoria ya existe'
end
go


Create proc sp_EliminarCategoria(
@IdCategoria int,
@Mensaje varchar(500) output,
@Resultado bit output
)
as 
begin
	set @Resultado = 0
	IF NOT EXISTS (select * from producto p inner join categoria c on c.IdCategoria = p.IdCategoria
	where p.IdCategoria = @IdCategoria)
	begin
		delete top (1) from categoria where IdCategoria = @IdCategoria
		set @Resultado = 1
	end
	else
		set @Mensaje = 'La categoria se encuentra relacionada a un producto'
end
go

--PROCEDIMIENTOS ALMACENADOS PRODUCTOS (AUN NO ESTAN EJECUTADOS - 1:17PM)

create proc sp_RegistrarProducto(
@Nombre varchar (100),
@Descripcion varchar (100),
@IdCategoria varchar(100),
@Precio decimal(10,2),
@Stock int,
@Activo bit,
@Mensaje varchar(100) output,
@Resultado int output
)
AS
begin
	SET @Resultado = 0
	IF NOT EXISTS(SELECT * FROM producto WHERE Nombre = @Nombre)
	begin
		insert into producto(Nombre, Descripcion, IdCategoria, Precio, Stock, Activo) values
		(@Nombre, @Descripcion, @IdCategoria, @Precio, @Stock, @Activo)

		set @Resultado = SCOPE_IDENTITY()
	end
	else 
		set @Mensaje = 'El producto ya existe'
end
go

create proc sp_EditarProducto(
@IdProducto int,
@Nombre varchar (100),
@Descripcion varchar (100),
@IdCategoria varchar(100),
@Precio decimal(10,2),
@Stock int,
@Activo bit,
@Mensaje varchar(100) output,
@Resultado int output
)
AS
begin
	set @Resultado = 0
	IF NOT EXISTS (select * from producto where Nombre = @Nombre and IdProducto != @IdProducto)
	begin

		update producto set
		Nombre = @Nombre,
		Descripcion = @Descripcion,
		IdCategoria = @IdCategoria,
		Precio = @Precio,
		Stock = @Stock,
		Activo = @Activo
		where IdProducto = @IdProducto

		set @Resultado = 1
	end
	else
		set @Mensaje = 'El producto ya existe'
end
go


create proc sp_EliminarProducto(
@IdProducto int,
@Mensaje varchar(500) output,
@Resultado bit output
)
as
begin
	set @Resultado = 0
	IF NOT EXISTS (select * from detalleVenta dv 
	inner join producto p on p.IdProducto = dv.IdProducto 
	where p.IdProducto = @IdProducto)
	begin
		delete top (1) from producto where IdProducto = @IdProducto
		set @Resultado = 1
	end
	else
		set @Mensaje = 'El producto se encuentra relacionado a una venta'
end
go


select p.IdProducto, p.Nombre, p.Descripcion,
c.IdCategoria, c.Descripcion[DesCategoria],
p.Precio, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo
from producto p
inner join categoria c on c.IdCategoria = p.IdCategoria
go

-- PROCEDIMIENTOS ALMACENADOS DASHBOARD --

CREATE PROC sp_ReporteDashboard
AS
BEGIN
SELECT 
	(SELECT COUNT(*) FROM cliente)[totalCliente],
	(SELECT ISNULL(SUM(Cantidad),0) FROM detalleVenta)[totalVenta],
	(SELECT COUNT(*)FROM producto)[totalProducto]
END

go

EXEC sp_ReporteDashboard
go


create proc sp_ReporteVentas(
@fechainicio varchar(10),
@fechafin varchar(10),
@idtransaccion varchar(50)
)
as
begin
set dateformat day;

select CONVERT(char(10), v.FechaVenta,103)[FechaVenta], CONCAT(c.Nombres,' ',c.Apellidos)[Cliente],
p.Nombre[Producto], p.Precio, dv.Cantidad, dv.Total, v.IdTransaccion
from detalleVenta dv
inner join producto p on p.IdProducto = dv.IdProducto
inner join venta v on v.IdVenta = dv.IdVenta
inner join cliente c on c.IdCliente = v.IdCliente

where CONVERT(date, v.FechaVenta) between @fechainicio and @fechafin
and v.IdTransaccion = iif(@idtransaccion = '', v.IdTransaccion, @idtransaccion)
end
go

-- PROCEDIMIENTO ALMACENADO TIENDA -- 

create proc sp_RegistrarCliente(
@Nombres varchar(100),
@Apellidos varchar(100),
@Correo varchar(100),
@Clave varchar(100),
@Mensaje varchar(500) output,
@Resultado int output
)
as
begin
	set @Resultado = 0
	IF NOT EXISTS (SELECT * FROM cliente where correo = @Correo)
	begin
		insert into cliente(Nombres, Apellidos, Correo, Clave, Reestablecer) values
		(@Nombres, @Apellidos, @Correo, @Clave, 0)

		set @Resultado = SCOPE_IDENTITY()
	end
	else
		set @Mensaje = 'El correo del usuario ya existe'
end
go


declare @idcategoria int = 0

select distinct c.IdCategoria, c.Descripcion from producto p
inner join categoria c on c.IdCategoria = p.IdCategoria and c.Activo = 1
where c.IdCategoria = iif(@idcategoria = 0, c.IdCategoria, @idcategoria)
go

select * from detalleVenta;

select * from venta;

select * from usuario;

select * from cliente;

create proc sp_ExisteCarrito(
@IdCliente int,
@IdProducto int,
@Resultado bit output
)
as
begin
	if exists(select * from carrito where IdCliente = @IdCliente and IdProducto = @IdProducto)
		set @Resultado = 1
	else
		set @Resultado = 0
end
go

create proc sp_OperacionCarrito(
@IdCliente int,
@IdProducto int,
@Sumar bit,
@Mensaje varchar(100) output,
@Resultado bit output
)
as
begin
	set @Resultado = 1
	set @Mensaje = ''

	declare @existecarrito bit = iif(exists(select * from carrito where IdCliente = @IdCliente and IdProducto = @IdProducto), 1,0)
	declare @stockproducto int = (select stock from producto where IdProducto = @IdProducto)

	begin try
		begin transaction operacion

		if(@Sumar = 1)
		begin

			if(@stockproducto > 0)

			begin
			if(@existecarrito = 1)
				update carrito set Cantidad = Cantidad + 1 where IdCliente = @IdCliente and IdProducto = @IdProducto
			else
				insert into carrito(IdCliente, IdProducto, Cantidad) values(@IdCliente, @IdProducto, 1)
			update producto set Stock = Stock + 1 where IdProducto = @IdProducto
			end
			else
			begin
				set @Resultado = 0
				set @Mensaje = 'El producto no cuenta con stock disponible'
			end
		end
		else 
		begin
			update carrito set Cantidad = Cantidad - 1 where IdCliente = @IdCliente and IdProducto = @IdProducto
			update producto set Stock = Stock + 1 where IdProducto = @IdProducto
		end

		commit transaction operacion

	end try
	begin catch
		set @Resultado = 0
		set @Mensaje = ERROR_MESSAGE()
		rollback transaction operacion
	end catch
end
go