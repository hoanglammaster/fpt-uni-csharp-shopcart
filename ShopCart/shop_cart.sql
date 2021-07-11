USE master
GO
CREATE DATABASE ShopBook
GO
USE ShopBook
GO

CREATE TABLE Users(
	UserId INT IDENTITY(1000,1),
	UserName VARCHAR(50) NOT NULL,
	UserPassword VARCHAR(50) NOT NULL,
	CONSTRAINT "PK_Users" PRIMARY KEY
	(
		"UserId"
	)
)
CREATE TABLE Books(
	BookId INT IDENTITY(1000,1),
	BookName NVARCHAR(200) NOT NULL,
	Prices MONEY NOT NULL,
	QuantityInStocks INT NOT NULL,
	CONSTRAINT "PK_Books" PRIMARY KEY
	(
		"BookId"
	)
)

CREATE TABLE Contacts(
	ContactId INT IDENTITY(1000,1),
	UserId INT NOT NULL,
	[Address] NVARCHAR(200) NULL,
	Phone VARCHAR(20) NULL,
	Email VARCHAR(30) NULL,
	CONSTRAINT "PK_Contacts" PRIMARY KEY
	(
		"ContactId"
	),
	CONSTRAINT "FK_Contacts_Users" FOREIGN KEY
	(
		"UserId"
	) REFERENCES "dbo"."Users"
	(
		"UserId"
	)
)
CREATE TABLE Carts(
	CartId INT IDENTITY(1000,1),
	UserId INT NOT NULL,
	CreatedDate DATETIME DEFAULT GETDATE(),
	CONSTRAINT "PK_Carts" PRIMARY KEY
	(
		"CartId"
	),
	CONSTRAINT "FK_Carts_Users" FOREIGN KEY
	(
		"UserId"
	) REFERENCES "dbo"."Users"
	(
		"UserId"
	)
)

CREATE TABLE CartDetails(
	CartDetailId INT IDENTITY(1000,1),
	CartId INT NOT NULL,
	BookId INT NOT NULL,
	Quantity INT NOT NULL,
	CONSTRAINT "PK_CartDetails" PRIMARY KEY
	(
		"CartDetailId"
	),
	CONSTRAINT "FK_CartDetails_Carts" FOREIGN KEY
	(
		"CartId"
	) REFERENCES "dbo"."Carts"
	(
		"CartId"
	),
	CONSTRAINT "FK_CartDetails_Books" FOREIGN KEY
	(
		"BookId"
	) REFERENCES "dbo"."Books"
	(
		"BookId"
	)
)

CREATE TABLE Images(
	ImageId INT IDENTITY(1000,1),
	BookId INT NOT NULL,
	ImageName NVARCHAR(100) NOT NULL,
	ImageURL NVARCHAR(200) NOT NULL,
	CONSTRAINT "PK_Images" PRIMARY KEY
	(
		"ImageId"
	),
	CONSTRAINT "FK_Images_Books" FOREIGN KEY
	(
		"BookId"
	) REFERENCES "dbo"."Books"
	(
		"BookId"
	)
)


--------------INSERT---------------
INSERT INTO Books VALUES( 'THE BAUHAUS BOOK' , 400.00 , 100 )
INSERT INTO Books VALUES( 'A RANGE OF POEMS BY GARY SNYDER' , 120.00 , 100 )
INSERT INTO Books VALUES( 'NEVER BEFORE, SO MUCH SO FEW BY ALAN HARRIS' , 55.00  , 100 )
INSERT INTO Books VALUES( 'POETRY NOW: CHARLES BUKOWSKI VOL. 1, NO. 6' , 50.00 , 100 )
INSERT INTO Books VALUES( 'ON THE WING BY ANNE WALDMAN / HIGHJACKING BY LEWIS WARSH' ,  60.00 , 100 )
INSERT INTO Books VALUES( 'POETS ON: COUPLING' , 22.00 , 100 )
INSERT INTO Books VALUES( 'CHRISTO POSTCARDS - PERSONAL POSTAGE' ,  275.00 , 100 )
INSERT INTO Books VALUES( 'SHOJI HAMADA: A POTTERS WAY AND WORK BY SUSAN PETERSON' , 35.00 , 100 )
INSERT INTO Books VALUES( 'SONGS OF WILD BIRDS BY ALBERT R. BRAND' , 50.00 , 100 )
INSERT INTO Books VALUES( 'NIGHT AND DAY BY DAVID ARMSTRONG' , 300.00 , 100 )
INSERT INTO Books VALUES( 'THE BROTHERS DUCHAMP BY PIERRE CABANNE' , 100.00 , 100 )


INSERT INTO Images VALUES(1000,'img_1','1.jpg')
INSERT INTO Images VALUES(1001,'img_2','2.jpg')
INSERT INTO Images VALUES(1002,'img_3','3.jpg')
INSERT INTO Images VALUES(1003,'img_4','4.jpg')
INSERT INTO Images VALUES(1004,'img_5','5.jpg')
INSERT INTO Images VALUES(1005,'img_6','6.jpg')
INSERT INTO Images VALUES(1006,'img_7','7.jpg')
INSERT INTO Images VALUES(1007,'img_8','8.jpg')
INSERT INTO Images VALUES(1008,'img_9','9.jpg')
INSERT INTO Images VALUES(1009,'img_10','10.jpg')
GO
ALTER TABLE Carts
ADD  Total MONEY 

INSERT INTO Users VALUES('admin','admin')
GO

CREATE PROCEDURE usp_InsertCart
@UserId INT, @BookId INT , @Quantity INT, @Cart INT, @Total MONEY
AS
BEGIN
	DECLARE @CartId INT
	IF @Cart IS NOT NULL
	BEGIN
		SET @CartId = @Cart
	END
	ELSE
	BEGIN
		INSERT INTO Carts (UserId, Total) VALUES(@UserId,@Total);
		SET @CartId = @@IDENTITY
	END
	INSERT INTO CartDetails VALUES(@CartId,@BookId,@Quantity)
	UPDATE Books SET Quantity = Quantity - @Quantity WHERE BookId = @BookId
	SELECT @CartId AS CartId
END
