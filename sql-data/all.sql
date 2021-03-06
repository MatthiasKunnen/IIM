INSERT INTO dbo.CURRICULAR (NAME) VALUES ('Aardrijkskunde');
INSERT INTO dbo.CURRICULAR (NAME) VALUES ('Geschiedenis');
INSERT INTO dbo.CURRICULAR (NAME) VALUES ('Sport');
INSERT INTO dbo.CURRICULAR (NAME) VALUES ('Technologische opvoeding');

INSERT INTO dbo.TARGETGROUP (NAME) VALUES ('Secundair');
INSERT INTO dbo.TARGETGROUP (NAME) VALUES ('Kleuters');

INSERT INTO dbo.FIRM (EMAIL, NAME, PHONENUMBER, WEBSITE) VALUES ('bedrijf@example.com', 'ProFarma', null, null);
INSERT INTO dbo.FIRM (EMAIL, NAME, PHONENUMBER, WEBSITE) VALUES ('info@decathlon.com', 'Decathlon', '056735656', null);
INSERT INTO dbo.FIRM (EMAIL, NAME, PHONENUMBER, WEBSITE) VALUES ('willaert@online.be', 'Verkleedkledij Willaert', '056713092', null);
INSERT INTO dbo.FIRM (EMAIL, NAME, PHONENUMBER, WEBSITE) VALUES ('info@synhaeve.be', 'Doe-het-Zelf Synhaeve', '047665', null);
INSERT INTO dbo.FIRM (EMAIL, NAME, PHONENUMBER, WEBSITE) VALUES ('dslkfj@kfdf.com', 'gg', null, null);

INSERT INTO dbo.MATERIAL (ARTICLENR, DESCRIPTION, ENCODING, NAME, PRICE, FIRMID) VALUES ('456897', 'American Football Ball Bullet Black', 'jpg', 'American Football Ball', 6.95, null);
INSERT INTO dbo.MATERIAL (ARTICLENR, DESCRIPTION, ENCODING, NAME, PRICE, FIRMID) VALUES ('465486', 'Roman Centurion Helmet Red top', 'jpg', 'Romeinse Helm', 95.00, null);
INSERT INTO dbo.MATERIAL (ARTICLENR, DESCRIPTION, ENCODING, NAME, PRICE, FIRMID) VALUES ('SH372', 'Een zeer stevige hamer', 'jpg', 'Stanley hamer', 15.00, null);

INSERT INTO dbo.MATERIALCURRICULAR (MATERIALID, CURRICULARID) VALUES (1, 1);
INSERT INTO dbo.MATERIALCURRICULAR (MATERIALID, CURRICULARID) VALUES (2, 2);
INSERT INTO dbo.MATERIALCURRICULAR (MATERIALID, CURRICULARID) VALUES (3, 3);
INSERT INTO dbo.MATERIALCURRICULAR (MATERIALID, CURRICULARID) VALUES (3, 4);

INSERT INTO dbo.MATERIALTARGETGROUP (MATERIALID, TARGETGROUPID) VALUES (2, 1);
INSERT INTO dbo.MATERIALTARGETGROUP (MATERIALID, TARGETGROUPID) VALUES (3, 1);

INSERT INTO dbo.MATERIALIDENTIFIER (PLACE, VISIBILITY, Material_Id) VALUES ('B1.0123', 0, 1);
INSERT INTO dbo.MATERIALIDENTIFIER (PLACE, VISIBILITY, Material_Id) VALUES ('B1.0123', 1, 2);
INSERT INTO dbo.MATERIALIDENTIFIER (PLACE, VISIBILITY, Material_Id) VALUES ('Materiaalkot', 2, 2);
INSERT INTO dbo.MATERIALIDENTIFIER (PLACE, VISIBILITY, Material_Id) VALUES ('Materiaalkot', 2, 2);

--The reservations require a user to be present
INSERT INTO dbo.RESERVATION (CREATIONDATE, ENDDATE, STARTDATE, USER_ID) VALUES ('2016-03-03', '2016-03-14', '2016-03-19', 1);
INSERT INTO dbo.RESERVATION (CREATIONDATE, ENDDATE, STARTDATE, USER_ID) VALUES ('2016-02-09', '2016-03-07', '2016-03-12', 1);

INSERT INTO dbo.ReservationDetail(RESERVATION_ID, MATERIALIDENTIFIERID) VALUES (1, 2);
INSERT INTO dbo.ReservationDetail (RESERVATION_ID, MATERIALIDENTIFIERID) VALUES (1, 3);
INSERT INTO dbo.ReservationDetail (RESERVATION_ID, MATERIALIDENTIFIERID) VALUES (2, 4);