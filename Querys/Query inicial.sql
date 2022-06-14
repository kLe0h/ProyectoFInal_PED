use ProyectoPED_FaseFinal

create table categoria(
IdCategoria int primary key identity,
Descripcion varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()
)

create table producto(
IdProducto int primary key identity,
Nombre varchar(500),
Descripcion varchar(500),
IdCategoria int references categoria(IdCategoria),
Precio decimal(10, 2) default 0,
Stock int,
RutaImagen varchar(100),
NombreImagen varchar(100),
Activo bit default 1,
FechaRegistro datetime default getdate()

)

create table cliente(
IdCliente int primary key identity,
Nombres varchar(100),
Apellidos varchar(100),
Correo varchar(100),
Clave varchar(100),
Reestablecer bit default 0,
FechaRegistro datetime default getdate()
)

create table carrito(
IdCarrito int primary key identity,
IdCliente int references cliente(IdCliente),
IdProducto int references producto(IdProducto),
Cantidad int
)

create table venta(
IdVenta int primary key identity,
IdCliente int references cliente(IdCliente),
TotalProducto int,
MontoTotal decimal(10,2),
Contacto varchar(50),
IdZona varchar(30), -- Zona donde se entrega el producto
Telefono varchar(50), -- Direccion exacta para envio del producto
Direccion varchar(500),
IdTransaccion varchar(50),
FechaVenta datetime default getdate()
)

create table detalleVenta(
IdDetalleVenta int primary key identity,
IdVenta int references venta(IdVenta),
IdProducto int references producto(IdProducto),
Cantidad int, -- Unidades del cliente
Total decimal(10,2)
)


-- Esta tabla usuario permite acceder al modo administrador
-- Por lo que se diferencia de la tabla cliente
create table usuario(
IdUsuario int primary key identity,
Nombres varchar(100),
Apellidos varchar(100),
Correo varchar(100),
Clave varchar(100),
Reestablecer bit default 1,
Activo bit default 1,
FechaRegistro datetime default getdate()
)

create table departamento(
	IdDepartamento varchar(2) NOT NULL,
	Descripcion varchar(45)
)

create table zona(
	IdZona varchar(50) NOT NULL,
	Descripcion varchar(45) NOT NULL,
	IdDepartamento varchar(2) NOT NULL
)
