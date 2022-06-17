

use ProyectoPED_FaseFinal;

go

select * from usuario;

-- Creando el procedimiento almacenado para registrar un usuario
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
