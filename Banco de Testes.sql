create database testDB

use testDB

drop table tblCliente

create table tblCliente(
	id int primary key,
    email varchar(80),
    senha char(30),
    telefone varchar(11),
    img LONGBLOB 
)

insert into tblCliente VALUES (1, 'Bia', '123')
insert into tblCliente VALUES (2, 'PedroLol', 'hiprocrita')

DELIMITER //
CREATE PROCEDURE Img_Upload (	
IN @email varchar(80),
    IN @senha char(30),
    IN @telefone varchar(11),
    IN @img varbinary(max)) READS SQL DATA
BEGIN
	insert into contatosImg	VALUES (
		@email,
		@senha,
		@telefone,
		@img
	)
END;
// DELIMITER ;

