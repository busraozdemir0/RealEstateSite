
-- PopularLocation tablosunda CityName alani benim Product tablosunda ekledigim city degerine esitse PropertyCount sayisini bir arttiran Trigger.

Create Trigger IncreaseLocationCount On Product
After Insert -- Product tablosunda ekleme isleminden sonra calisacak olan trigger
As
Declare @city nvarchar(50)
Select @city=City From inserted -- @city degiskeni benim yeni ekledigim City degerine karsilik gelen deger
Update PopularLocation Set PropertyCount+=1 Where CityName=@city -- PopularLocation tablosunda CityName alani benim ekledigim city degerine esitse PropertyCount sayisini bir arttir.


-- Product tablosunda guncelleme isleminden sonra calisacak olan trigger

Create Trigger IncreaseLocationCountUpdate On Product
After Update 
As
Declare @city nvarchar(50)
Select @city=City From inserted -- 
Update PopularLocation Set PropertyCount+=1 Where CityName=@city 