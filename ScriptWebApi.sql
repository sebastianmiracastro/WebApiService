CREATE SCHEMA clientst;
USE clientst;

CREATE TABLE client(
	id int(10) primary key not null,
	Name varchar(50),
    Last_Name varchar(50),
	Document_ID int(15)
);

CREATE TABLE invoice(
	id_invoice int(10) primary key not null,
	ID_Client int(10),
	Cod int(10)
);

CREATE TABLE invoicedetail(
	ID_Detail int(10) primary key not null,
	ID_Invoice int(10),
    Description varchar(200),
	Value char(50)
);


ALTER TABLE invoicedetail ADD FOREIGN KEY (ID_Invoice) REFERENCES invoice(id_invoice);
ALTER TABLE invoice ADD FOREIGN KEY (ID_Client) REFERENCES client(id);