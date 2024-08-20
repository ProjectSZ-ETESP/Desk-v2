create database testDB

use testDB

drop table tblCliente

create table tblCliente(
	id int primary key AUTO_INCREMENT,
    email varchar(80),
    senha char(30),
    telefone varchar(11),
    img MEDIUMBLOB
)

insert into tblCliente (email, senha) VALUES ('Bia', '123')
insert into tblCliente VALUES (2, 'PedroLol', 'hiprocrita')

DELIMITER $$
CREATE PROCEDURE Img_Upload (	
	IN idRec int,
	
    IN byteIMG LONGBLOB) READS SQL DATA
BEGIN
	update tblCliente 
    set img = byteIMG
    where id = idRec;
END $$
DELIMITER ;

select * from tblCliente;


call Img_Upload(1, 5678)

