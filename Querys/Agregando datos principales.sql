use ProyectoPED_FaseFinal

-- Ingresando datos predefinidos para las tablas a usar.

select * from usuario;

INSERT INTO usuario (Nombres, Apellidos, Correo, Clave) values ('test nombre', 'test apellido', 'test@gmail.com', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3');

select * from categoria;

INSERT INTO categoria(descripcion) values ('Postres'), ('Galletas'), ('Dulces'), ('Especiales')

-- Ingresando departamentos a usar para nuestro sistema.
select * from departamento;

insert into departamento(IdDepartamento, Descripcion) values
('01', 'San Salvador'), ('02', 'La paz'), ('03', 'Usulután'), ('04', 'Santa Ana'), ('05', 'Cuscatlán'),
('06', 'San Vicente'), ('07', 'San Miguel'), ('08', 'Morazán'), ('09', 'La unión'), ('10','Ahuachapán'),
('11', 'Chalatenango'), ('12', 'Cabañas'), ('13', 'Sonsonate'), ('14', 'La libertad')

-- Ingresando las zonas
select * from zona;

insert into zona(IdZona, Descripcion, IdDepartamento) values
('0101', 'Zona central', '01'), ('0102','Zona central','14'), ('0103', 'Zona Central', '05'),
('0201', 'Zona occidental', '04'), ('0202','Zona occidental','10'),
('0301', 'Zona paracentral', '02'), ('0302', 'Zona paracentral', '12'),
('0401', 'Zona oriental', '03'), ('0402', 'Zona oriental', '07')