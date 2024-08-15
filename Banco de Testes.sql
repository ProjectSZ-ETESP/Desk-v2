create database testDB

use testDB

create table tblCliente(
	id int primary key,
    nome varchar(80),
    senha char(30)
)

insert into tblCliente VALUES (1, 'Bia', '123')