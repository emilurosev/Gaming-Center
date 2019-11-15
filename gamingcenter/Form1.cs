using System;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace gamingcenter
{
    public partial class Form1 : Form
    {
        static string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=root;database=gaming_center";
        MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
        public Form1()
        {
            InitializeComponent();
            onloadCb();
           
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("");

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        
        private void createTables()
        {
            string q1 = "CREATE TABLE IF NOT EXISTS osobe(id INT(11) NOT NULL AUTO_INCREMENT,ime VARCHAR(255) NOT NULL,prezime VARCHAR(255) NOT NULL,korisnickoIme VARCHAR(255) NOT NULL,lozinka VARCHAR(255) NOT NULL,email VARCHAR(255) NOT NULL,brojTelefona VARCHAR(255),pol VARCHAR(255) NOT NULL,datumRodjenja DATE NOT NULL,PRIMARY KEY(id));";
            string q2 = "CREATE TABLE IF NOT EXISTS menadzeri(id INT(11) NOT NULL AUTO_INCREMENT,pocetakRadnogOdnosa DATE NOT NULL,plata INT(11) NOT NULL,godisnjiBonus INT(11), idOsobe INT(11) NOT NULL,PRIMARY KEY(id),FOREIGN KEY(idOsobe) REFERENCES osobe(id));";
            string q3 = "CREATE TABLE IF NOT EXISTS poslovnice(id INT(11) NOT NULL AUTO_INCREMENT,naziv VARCHAR(255) NOT NULL,grad VARCHAR(255) NOT NULL,drzava VARCHAR(255) NOT NULL,ulica VARCHAR(255) NOT NULL,brojTelefona VARCHAR(255) NOT NULL, email VARCHAR(255) NOT NULL,radnoVreme VARCHAR(255),aktivna BOOLEAN DEFAULT 1,idMenadzera INT(11) NOT NULL,PRIMARY KEY(id),FOREIGN KEY(idMenadzera) REFERENCES menadzeri(id));";
            string q4 = "CREATE TABLE IF NOT EXISTS kupci(id INT(11) NOT NULL AUTO_INCREMENT,drzava VARCHAR(255) NOT NULL,grad VARCHAR(255) NOT NULL, adresa VARCHAR(255) NOT NULL, ukupanTrosak INT(11),idOsobe INT(11) NOT NULL,PRIMARY KEY(id),FOREIGN KEY(idOsobe) REFERENCES osobe(id));";
            string q5 = "CREATE TABLE IF NOT EXISTS prodavci(id INT(11) NOT NULL AUTO_INCREMENT,pocetakRadnogOdnosa DATE NOT NULL, plata INT(11) NOT NULL, godisnjiBonus INT(11), idOsobe INT(11) NOT NULL,idPoslovnice INT(11) NOT NULL,PRIMARY KEY(id),FOREIGN KEY(idOsobe) REFERENCES osobe(id),FOREIGN KEY(idPoslovnice) REFERENCES poslovnice(id));";
            string q6 = "CREATE TABLE IF NOT EXISTS proizvodi(id INT(11) NOT NULL AUTO_INCREMENT, preostalo INT(11) NOT NULL,cena INT(11) NOT NULL,popust INT(11) DEFAULT 0, ukupnoProdato INT(11) DEFAULT 0,ukupnaZarada INT(11) DEFAULT ((cena-popust)*ukupnoProdato),komentar VARCHAR(255),PRIMARY KEY(id));";
            string q7 = "CREATE TABLE IF NOT EXISTS igrice(id INT(11) NOT NULL AUTO_INCREMENT,naziv VARCHAR(255) NOT NULL,platforma VARCHAR(255) NOT NULL, zanr VARCHAR(255) NOT NULL,opis VARCHAR(255),datumIzlaska DATE NOT NULL,idProizvoda INT(11) NOT NULL,PRIMARY KEY(id),FOREIGN KEY(idProizvoda) REFERENCES proizvodi(id));";
            string q8 = "CREATE TABLE IF NOT EXISTS filmovi(id INT(11) NOT NULL AUTO_INCREMENT,naziv VARCHAR(255) NOT NULL,zanr VARCHAR(255) NOT NULL,izdavackaKuca VARCHAR(255),datumIzlaska DATE NOT NULL,opis VARCHAR(255),idProizvoda INT(11) NOT NULL,PRIMARY KEY(id),FOREIGN KEY(idProizvoda) REFERENCES proizvodi(id));";
            string q9 = "CREATE TABLE IF NOT EXISTS albumi(id INT(11) NOT NULL AUTO_INCREMENT,naziv VARCHAR(255) NOT NULL,zanr VARCHAR(255) NOT NULL,izdavackaKuca VARCHAR(255),ukupnoTrajanjeMin INT(11),brojPesama INT(11),datumIzlaska DATE NOT NULL,opis VARCHAR(255),idProizvoda INT(11) NOT NULL,PRIMARY KEY(id),FOREIGN KEY(idProizvoda) REFERENCES proizvodi(id));";
            string q10 = "CREATE TABLE IF NOT EXISTS racunari(id INT(11) NOT NULL AUTO_INCREMENT,cpu VARCHAR(255) NOT NULL,maticnaPloca VARCHAR(255) NOT NULL,gpu VARCHAR(255) NOT NULL,ram VARCHAR(255) NOT NULL,hdd VARCHAR(255) NOT NULL,zvucnaKartica VARCHAR(255),napajanje VARCHAR(255),idProizvoda INT(11) NOT NULL,PRIMARY KEY(id),FOREIGN KEY(idProizvoda) REFERENCES proizvodi(id));";
            string q11 = "CREATE TABLE IF NOT EXISTS konzole(id INT(11) NOT NULL AUTO_INCREMENT,proizvodjac VARCHAR(255) NOT NULL,model VARCHAR(255) NOT NULL,idProizvoda INT(11) NOT NULL,PRIMARY KEY(id),FOREIGN KEY(idProizvoda) REFERENCES proizvodi(id));";
            string q12 = "CREATE TABLE IF NOT EXISTS narudzbine(id INT(11) NOT NULL AUTO_INCREMENT, idKupca INT(11) NOT NULL,idProdavca INT(11) NOT NULL,idAlbuma INT(11),idFilma INT(11),idIgrice INT(11),idKonzole INT(11),idRacunara INT(11),iznos INT(11) NOT NULL,kolicina INT(11) DEFAULT 1,datum DATE NOT NULL,PRIMARY KEY(id),FOREIGN KEY(idKupca) REFERENCES kupci(id),FOREIGN KEY(idProdavca) REFERENCES prodavci(id),FOREIGN KEY(idAlbuma) REFERENCES albumi(id),FOREIGN KEY(idFilma) REFERENCES filmovi(id),FOREIGN KEY(idIgrice) REFERENCES igrice(id),FOREIGN KEY(idKonzole) REFERENCES konzole(id),FOREIGN KEY(idRacunara) REFERENCES racunari(id));";



            string[] queries = new string[12] { q1, q2, q3, q4, q5, q6, q7, q8, q9, q10, q11, q12 };

            for (int i = 0; i<12; i++)
            {
                Console.WriteLine(i);
                Console.WriteLine(queries[i]);
                runQuery(queries[i]);
                databaseConnection.Close();
                if (i == 11)
                {
                    MessageBox.Show("Success");
                }
            }

            
        }

        MySqlDataReader myReader;

        private void fillTable()
        {
            string q1 = "INSERT INTO osobe(`id`, `ime`, `prezime`, `korisnickoIme`, `lozinka`, `email`, `brojTelefona`, `pol`, `datumRodjenja`) VALUES(NULL,'Pera','Peric','perap','1234','pera@gmail.com','063-521-449','muski','1985-06-06'),(NULL,'Jova','Jovic','jovaj','1234','jova@gmail.com','063-621-449','muski','1988-02-16'),(NULL,'Ivan','Ivanovic','ivani','1234','ivan@gmail.com','063-555-222','muski','1990-11-10'),(NULL,'Kristina','Nikolic','kristinan','1234','kristina@gmail.com','068-222-111','zenski','1994-09-15'),(NULL,'Jovana','Markovic','jovanam','1234','jovana@gmail.com','063-111-222','zenski','1985-06-06'),(NULL,'Marko','Markovic','markom','1234','marko@gmail.com','063-777-231','muski','1996-01-15'),(NULL,'Milan','Milanovic','milanm','1234','milan@gmail.com','062-555-449','muski','1996-02-07'),(NULL,'Bojana','Brankovic','bojanab','1234','bojana@gmail.com','069-1234-567','zenski','1997-03-28'),(NULL,'Djordje','Djordjevic','djordjedj','1234','djoka@gmail.com','063-552-255','muski','1980-01-01'),(NULL,'Tamara','Ivic','tamarai','1234','tamara@gmail.com','066-121-459','zenski','1997-11-01'),(NULL,'Luka','Lukovic','lukal','1234','luka@gmail.com','069-111-334','muski','1995-02-06'),(NULL,'Bojan','Stojanovic','bojans','1234','boki@gmail.com','064-991-449','muski','1996-05-07'),(NULL,'Nikola','Marjanovic','marjan','1234','marjan@gmail.com','060-888-442','muski','1996-05-05'),(NULL,'Marina','Petrovic','marinap','1234','marina@gmail.com','066-314-653','zenski','1982-08-01'),(NULL,'Nevena','Todorovic','nevenat','1234','nevena@gmail.com','061-441-7889','zenski','1992-01-07'),(NULL,'Adrijano','Davidov','adrijanod','1234','adri@gmail.com','064-2425-745','muski','1999-10-07'),(NULL,'Andrej','Vidovic','andrejv','1234','andrej@gmail.com','066-5262-149','muski','1989-10-17'),(NULL,'Nemanja','Vlatkovic','nemanjav','1234','neca@gmail.com','065-135-856','muski','1996-05-25'),(NULL,'Danilo','Vlatkovic','danilov','1234','daca@gmail.com','062-185-163','muski','1992-03-01'),(NULL,'Branislav','Nikolic','branislavn','1234','bane@gmail.com','067-1235-548','muski','1979-05-16'),(NULL,'Ivko','Novakovic','ivkon','1234','ivko@gmail.com','067-1521-479','muski','1998-12-05'),(NULL,'Nedeljko','Jovicic','nedeljkoj','1234','nedeljko@gmail.com','061-821-449','muski','1990-01-16'),(NULL,'Ivana','Ivankovic','ivanai','1234','ivana@gmail.com','061-775-2152','zenski','1995-04-27'),(NULL,'Kristina','Jokovic','kristinaj','1234','kristinaj@gmail.com','061-5162-174','zenski','1992-10-02'),(NULL,'Ema','Belic','emab','1234','ema@gmail.com','061-1256-228','zenski','1997-01-02'),(NULL,'imeKupca1','prezimeKupca1','kupac1','1234','k1@gmail.com',NULL,'muski','1995-06-06'),(NULL,'imeKupca2','prezimeKupca2','kupac2','1234','k2@gmail.com',NULL,'zenski','1985-02-02'),(NULL,'imeKupca3','prezimeKupca3','kupac3','1234','k3@gmail.com',NULL,'muski','1998-01-01'),(NULL,'imeKupca4','prezimeKupca4','kupac4','1234','k4@gmail.com',NULL,'zenski','2004-03-03'),(NULL,'imeKupca5','prezimeKupca5','kupac5','1234','k5@gmail.com',NULL,'muski','1990-04-04'),(NULL,'imeKupca6','prezimeKupca6','kupac6','1234','k6@gmail.com',NULL,'zenski','1991-05-05'),(NULL,'imeKupca7','prezimeKupca7','kupac7','1234','k7@gmail.com',NULL,'muski','1993-02-08'),(NULL,'imeKupca8','prezimeKupca8','kupac8','1234','k8@gmail.com',NULL,'muski','1985-06-22'),(NULL,'imeKupca9','prezimeKupca9','kupac9','1234','k9@gmail.com',NULL,'zenski','1999-08-29'),(NULL,'imeKupca10','prezimeKupca10','kupac10','1234','k10@gmail.com',NULL,'zenski','1999-12-12'),(NULL,'imeKupca11','prezimeKupca11','kupac11','1234','k11@gmail.com',NULL,'muski','1998-05-01'),(NULL,'imeKupca12','prezimeKupca12','kupac12','1234','k12@gmail.com',NULL,'zenski','1986-10-18'),(NULL,'imeKupca13','prezimeKupca13','kupac13','1234','k13@gmail.com',NULL,'muski','1996-01-06'),(NULL,'imeKupca14','prezimeKupca14','kupac14','1234','k14@gmail.com',NULL,'muski','1997-02-01'),(NULL,'imeKupca15','prezimeKupca15','kupac15','1234','k15@gmail.com',NULL,'muski','1985-06-26'),(NULL,'imeKupca16','prezimeKupca16','kupac16','1234','k16@gmail.com',NULL,'zenski','1979-01-02'),(NULL,'imeKupca17','prezimeKupca17','kupac17','1234','k17@gmail.com',NULL,'zenski','1994-01-06'),(NULL,'imeKupca18','prezimeKupca18','kupac18','1234','k18@gmail.com',NULL,'muski','1984-10-11'),(NULL,'imeKupca19','prezimeKupca19','kupac19','1234','k19@gmail.com',NULL,'muski','1995-03-10'),(NULL,'imeKupca20','prezimeKupca20','kupac20','1234','k20@gmail.com',NULL,'zenski','1992-01-029'),(NULL,'imeKupca21','prezimeKupca21','kupac21','1234','k21@gmail.com',NULL,'muski','1992-05-20'),(NULL,'imeKupca22','prezimeKupca22','kupac22','1234','k22@gmail.com',NULL,'muski','198-10-27');";
            runQuery(q1);
            databaseConnection.Close();

            string q2 = "INSERT INTO `menadzeri`(`id`, `pocetakRadnogOdnosa`, `plata`, `godisnjiBonus`, `idOsobe`) VALUES (NULL,'2015-01-05',120000,30000,1),(NULL,'2012-07-23',110000,40000,2),(NULL,'2014-02-23',130000,30000,3),(NULL,'2016-02-10',130000,10000,4),(NULL,'2015-01-05',150000,30000,5);";
            runQuery(q2);
            databaseConnection.Close();

            string q3 = "INSERT INTO `poslovnice`(`id`, `naziv`, `grad`, `drzava`, `ulica`, `brojTelefona`, `email`, `radnoVreme`, `aktivna`, idMenadzera) VALUES(NULL,'Gaming centar NS1','Novi Sad','Srbija','Micurinova 66','021-525-124','gcns1@gmail.com','08:00 - 20:00',DEFAULT, 1),(NULL,'Gaming centar NS2','Novi Sad','Srbija','Alekse Santica 30','021-115-124','gcns2@gmail.com','08:00 - 20:00',DEFAULT, 1),(NULL,'Gaming centar NS3','Novi Sad','Srbija','Bogdana Garabantina 12','021-562-126','gcns3@gmail.com','08:00 - 20:00',DEFAULT, 1),(NULL,'Gaming centar BG1','Beograd','Srbija','Knez Mihajlova 12','011-556-001','gcbg1@gmail.com','08:00 - 20:00',DEFAULT, 2),(NULL,'Gaming centar BG2','Beograd','Srbija','Gandijeva 166','011-888-222','gcbg2@gmail.com','08:00 - 20:00',DEFAULT, 2),(NULL,'Gaming centar BG3','Beograd','Srbija','Marsala Birjuzova 5','011-415-100','gcbg3@gmail.com','08:00 - 20:00',DEFAULT, 3),(NULL,'Gaming centar BG4','Beograd','Srbija','Ulica 124','011-777-1213','gcbg4@gmail.com','08:00 - 20:00',DEFAULT, 3),(NULL,'Gaming centar NIS1','Nis','Srbija','Ulica 14','018-621-514','gcnis1@gmail.com','08:00 - 20:00',DEFAULT, 4),(NULL,'Gaming centar NIS2','Nis','Srbija','Ulica2 151','018-225-522','gcnis2@gmail.com','08:00 - 20:00',DEFAULT, 4),(NULL,'Gaming centar KG1','Kragujevac','Srbija','Ulica3 66','034-155-6724','gckg1@gmail.com','08:00 - 20:00',DEFAULT, 5);";
            runQuery(q3);
            databaseConnection.Close();

            string q4 = "INSERT INTO `prodavci`(`id`, `pocetakRadnogOdnosa`, `plata`, `godisnjiBonus`, `idOsobe`, `idPoslovnice`) VALUES(NULL,'2016-01-02',50000,5000,6,1),(NULL,'2016-01-02',50000,5000,7,1),(NULL,'2017-01-02',50000,5000,8,2),(NULL,'2013-06-05',50000,5000,9,2),(NULL,'2014-07-22',50000,5000,10,3),(NULL,'2015-08-08',50000,5000,11,3),(NULL,'2018-05-30',50000,5000,12,4),(NULL,'2019-01-01',50000,5000,13,4),(NULL,'2012-07-25',50000,5000,14,5),(NULL,'2012-01-02',50000,5000,15,5),(NULL,'2019-05-06',50000,5000,16,6),(NULL,'2018-04-01',50000,5000,17,6),(NULL,'2015-10-06',50000,5000,18,7),(NULL,'2017-11-30',50000,5000,19,7),(NULL,'2018-05-22',50000,5000,20,8),(NULL,'2019-05-06',50000,5000,21,8),(NULL,'2018-04-01',50000,5000,22,9),(NULL,'2015-10-06',50000,5000,23,9),(NULL,'2017-11-30',50000,5000,24,10),(NULL,'2018-05-22',50000,5000,25,10);";
            runQuery(q4);
            databaseConnection.Close();

            string q5 = "INSERT INTO `kupci`(`id`, `drzava`, `grad`, `adresa`, `ukupanTrosak`, `idOsobe`) VALUES(NULL,'Srbija','Novi Sad','Adresa 1',1500,26),(NULL,'Srbija','Novi Sad','Adresa 2',2500,27),(NULL,'Srbija','Beograd','Adresa 3',3000,28),(NULL,'BiH','Sarajevo','Adresa 4',2000,29),(NULL,'Srbija','Nis','Adresa 5',15500,30),(NULL,'Srbija','Kragujevac','Adresa 6',200000,31),(NULL,'Srbija','Vrsac','Adresa 7',500,32),(NULL,'Srbija','Novi Sad','Adresa 8',132000,33),(NULL,'Srbija','Beograd','Adresa 9',80000,34),(NULL,'Srbija','Vrsac','Adresa 10',8000,35),(NULL,'Srbija','Beograd','Adresa 11',5000,36),(NULL,'BiH','Banja Luka','Adresa 12',31000,37),(NULL,'Srbija','Vrsac','Adresa 13',90000,38),(NULL,'Srbija','Zajecar','Adresa 14',75000,39),(NULL,'Srbija','Novi Sad','Adresa 15',2000,40),(NULL,'Srbija','Beograd','Adresa 16',5000,41),(NULL,'Srbija','Nis','Adresa 17',69000,42),(NULL,'Srbija','Nis','Adresa 18',423000,43),(NULL,'Srbija','Kragujevac','Adresa 19',100000,44),(NULL,'Srbija','Beograd','Adresa 20',900,45),(NULL,'Srbija','Novi Sad','Adresa 21',14500,46),(NULL,'Srbija','Negotin','Adresa 22',80000,47);";
            runQuery(q5);
            databaseConnection.Close();

            string q6 = "INSERT INTO `proizvodi`(`id`, `preostalo`, `cena`, `popust`, `ukupnoProdato`, `ukupnaZarada`, `komentar`) VALUES(NULL,20,2600,DEFAULT,12,DEFAULT,NULL),(NULL,20,1600,DEFAULT,21,DEFAULT,NULL),(NULL,0,2000,DEFAULT,46,DEFAULT,NULL),(NULL,0,1000,DEFAULT,50,DEFAULT,NULL),(NULL,4,1400,DEFAULT,21,DEFAULT,NULL),(NULL,0,1500,DEFAULT,20,DEFAULT,NULL),(NULL,10,600,DEFAULT,110,DEFAULT,NULL),(NULL,81,300,DEFAULT,19,DEFAULT,NULL),(NULL,10,600,DEFAULT,15,DEFAULT,NULL),(NULL,9,800,DEFAULT,32,DEFAULT,NULL),(NULL,20,500,DEFAULT,33,DEFAULT,NULL),(NULL,17,600,DEFAULT,15,DEFAULT,NULL),(NULL,5,600,DEFAULT,46,DEFAULT,NULL),(NULL,9,750,DEFAULT,66,DEFAULT,NULL),(NULL,20,1500,DEFAULT,13,DEFAULT,NULL),(NULL,20,2600,DEFAULT,12,DEFAULT,NULL),(NULL,20,1600,DEFAULT,21,DEFAULT,NULL),(NULL,0,2000,DEFAULT,46,DEFAULT,NULL),(NULL,0,1000,DEFAULT,50,DEFAULT,NULL),(NULL,4,1400,DEFAULT,21,DEFAULT,NULL),(NULL,0,1500,DEFAULT,20,DEFAULT,NULL),(NULL,10,600,DEFAULT,110,DEFAULT,NULL),(NULL,81,300,DEFAULT,19,DEFAULT,NULL),(NULL,10,600,DEFAULT,15,DEFAULT,NULL),(NULL,9,800,DEFAULT,32,DEFAULT,NULL),(NULL,20,500,DEFAULT,33,DEFAULT,NULL),(NULL,17,600,DEFAULT,15,DEFAULT,NULL),(NULL,14,800,DEFAULT,55,DEFAULT,NULL),(NULL,5,600,DEFAULT,46,DEFAULT,NULL),(NULL,15,1200,DEFAULT,26,DEFAULT,NULL),(NULL,20,1500,DEFAULT,12,DEFAULT,NULL),(NULL,20,2000,DEFAULT,21,DEFAULT,NULL),(NULL,0,2500,DEFAULT,46,DEFAULT,NULL),(NULL,0,3000,DEFAULT,50,DEFAULT,NULL),(NULL,4,3500,DEFAULT,21,DEFAULT,NULL),(NULL,0,4000,DEFAULT,20,DEFAULT,NULL),(NULL,10,4500,DEFAULT,110,DEFAULT,NULL),(NULL,81,6000,DEFAULT,19,DEFAULT,NULL),(NULL,10,1500,DEFAULT,15,DEFAULT,NULL),(NULL,9,2000,DEFAULT,32,DEFAULT,NULL),(NULL,20,2500,DEFAULT,33,DEFAULT,NULL),(NULL,17,3000,DEFAULT,15,DEFAULT,NULL),(NULL,5,3500,DEFAULT,46,DEFAULT,NULL),(NULL,9,4000,DEFAULT,66,DEFAULT,NULL),(NULL,20,4500,DEFAULT,13,DEFAULT,NULL),(NULL,20,15000,DEFAULT,12,DEFAULT,NULL),(NULL,20,25000,DEFAULT,21,DEFAULT,NULL),(NULL,0,30000,DEFAULT,46,DEFAULT,NULL),(NULL,0,50000,DEFAULT,50,DEFAULT,NULL),(NULL,4,50000,DEFAULT,21,DEFAULT,NULL),(NULL,0,55000,DEFAULT,20,DEFAULT,NULL),(NULL,10,30000,DEFAULT,110,DEFAULT,NULL),(NULL,81,35000,DEFAULT,19,DEFAULT,NULL),(NULL,10,24000,DEFAULT,15,DEFAULT,NULL),(NULL,9,50000,DEFAULT,32,DEFAULT,NULL),(NULL,20,50000,DEFAULT,33,DEFAULT,NULL),(NULL,17,30000,DEFAULT,15,DEFAULT,NULL),(NULL,5,45000,DEFAULT,46,DEFAULT,NULL),(NULL,9,55000,DEFAULT,66,DEFAULT,NULL),(NULL,20,66000,DEFAULT,13,DEFAULT,NULL),(NULL,20,25000,DEFAULT,12,DEFAULT,NULL),(NULL,20,30000,DEFAULT,21,DEFAULT,NULL),(NULL,0,35000,DEFAULT,46,DEFAULT,NULL),(NULL,0,40000,DEFAULT,50,DEFAULT,NULL),(NULL,4,45000,DEFAULT,21,DEFAULT,NULL),(NULL,0,50000,DEFAULT,20,DEFAULT,NULL),(NULL,10,55000,DEFAULT,110,DEFAULT,NULL),(NULL,81,60000,DEFAULT,19,DEFAULT,NULL),(NULL,10,65000,DEFAULT,15,DEFAULT,NULL),(NULL,9,70000,DEFAULT,32,DEFAULT,NULL),(NULL,20,75000,DEFAULT,33,DEFAULT,NULL),(NULL,17,80000,DEFAULT,15,DEFAULT,NULL),(NULL,5,85000,DEFAULT,46,DEFAULT,NULL),(NULL,9,100000,DEFAULT,66,DEFAULT,NULL),(NULL,20,66000,DEFAULT,13,DEFAULT,NULL);";
            runQuery(q6);
            databaseConnection.Close();

            string q7 = "INSERT INTO `albumi`(`id`, `naziv`, `zanr`, `izdavackaKuca`, `ukupnoTrajanjeMin`, `brojPesama`, `datumIzlaska`, `opis`, `idProizvoda`) VALUES(NULL,'Rihanna - Good girl gone bad','pop',NULL,90,15,'2012-01-01',NULL,1),(NULL,'Linkin Park - Meteora','rock',NULL,90,15,'2013-01-01',NULL,2),(NULL,'Eminem - Marshall Mathers LP','hip hop',NULL,90,15,'2002-01-01',NULL,3),(NULL,'Johnny Cash - Greatest','folk',NULL,90,15,'1960-01-01',NULL,4),(NULL,'David Guetta - Mixtape','dance',NULL,90,15,'2016-01-01',NULL,5),(NULL,'Adam Beyer - Mixtape','techno',NULL,90,15,'2017-01-01',NULL,6),(NULL,'Carl Cox - Mixtape','techno',NULL,90,15,'2018-01-01',NULL,7),(NULL,'50cent - Get Rich or Die Tryin','hip hop',NULL,90,15,'2011-01-01',NULL,8),(NULL,'Motorhead - Best of','metal',NULL,90,15,'1980-01-01',NULL,9),(NULL,'Linkin Park - Encore','rock',NULL,90,15,'2013-01-01',NULL,10),(NULL,'Metallica - Greatest hits','rock',NULL,90,15,'2014-01-01',NULL,11),(NULL,'Eminem - Marshall Mathers LP2','hip hop',NULL,90,15,'2015-01-01',NULL,12),(NULL,'Best movie music mixtape','klasicna muzika',NULL,90,15,'2016-01-01',NULL,13),(NULL,'AKON - Konvicted','rnb',NULL,90,15,'2017-01-01',NULL,14),(NULL,'Chance the rapper - Acid rap','rnb/hip hop',NULL,90,15,'2019-01-01',NULL,15);";
            runQuery(q7);
            databaseConnection.Close();

            string q8 = "INSERT INTO `filmovi`(`id`, `naziv`, `zanr`, `izdavackaKuca`, `datumIzlaska`, `opis`, `idProizvoda`) VALUES (NULL,'Rambo','Akcioni',NULL,'2005-05-05',NULL,16),(NULL,'Pogresno skretanje','Horor',NULL,'2006-05-05',NULL,17),(NULL,'Gas do daske 1','Komedija',NULL,'2004-05-05',NULL,18),(NULL,'Prica o vitezu','Drama',NULL,'2007-05-05',NULL,19),(NULL,'Roki 4','Drama',NULL,'2009-05-05',NULL,20),(NULL,'Cahill U.S. Marshal','Western',NULL,'1973-05-05',NULL,21),(NULL,'Pogresno skretanje 2','Horor',NULL,'2010-05-05',NULL,22),(NULL,'Grad duhova','Horor',NULL,'2010-05-05',NULL,23),(NULL,'Paklene ulice 3','Akcioni',NULL,'2011-05-05',NULL,24),(NULL,'Kako sam upoznao vasu majku','Ljubavni',NULL,'2005-05-05',NULL,25),(NULL,'Roki 1','Drama',NULL,'1980-05-05',NULL,26),(NULL,'Inception','Misterija',NULL,'2012-05-05',NULL,27),(NULL,'Igra','Misterija',NULL,'1990-05-05',NULL,28),(NULL,'27','Misterija',NULL,'2005-05-05',NULL,29),(NULL,'Gas do daske 3','Komedija',NULL,'2009-05-05',NULL,30);";
            runQuery(q8);
            databaseConnection.Close();

            string q9 = "INSERT INTO `igrice`(`id`, `naziv`, `platforma`, `zanr`, `opis`, `datumIzlaska`, `idProizvoda`) VALUES(NULL,'Diablo 3','PC','RPG',NULL,'2017-01-01',31),(NULL,'FIFA 18','PS4','Sport',NULL,'2018-01-01',32),(NULL,'Call of Duty 8','Xbox 360','Akcija',NULL,'2019-01-01',33),(NULL,'Spiderman','PS Vita','RPG',NULL,'2016-01-01',34),(NULL,'Starcarft: Remake','PC','RPG',NULL,'2017-01-01',35),(NULL,'NHL 18','PC','Sport',NULL,'2018-01-01',36),(NULL,'PES 19','PC','Sport',NULL,'2019-01-01',37),(NULL,'World of Warcraft','PS4','MMORPG',NULL,'2015-01-01',38),(NULL,'God of war','PS4','RPG',NULL,'2016-01-01',39),(NULL,'NBA 2017','Xbox 360','Sport',NULL,'2017-01-01',40),(NULL,'Batman','Xbox 360','RPG',NULL,'2018-01-01',41),(NULL,'Tenis','Wii','Sport',NULL,'2019-01-01',42),(NULL,'Guild Wars: New','PC','RPG',NULL,'2018-01-01',43),(NULL,'Spiderman 2','PS Vita','RPG',NULL,'2019-01-01',44),(NULL,'Warcraft: Remake','PC','Strategija',NULL,'2017-01-01',45);";
            runQuery(q9);
            databaseConnection.Close();

            string q10 = "INSERT INTO `konzole`(`id`, `proizvodjac`, `model`, `idProizvoda`) VALUES (NULL,'Sony','Playstation 2',46),(NULL,'Sony','Playstation 2 GOLD',47),(NULL,'Sony','Playstation 3 sa dodatnom memorijom',48),(NULL,'Sony','Playstation 4',49),(NULL,'Sony','Playstation 4 WHITE',50),(NULL,'Microsoft','Xbox 360',51),(NULL,'Microsoft','Xbox 360 GOLD',52),(NULL,'Sony','Playstation 3 sa 2 igrice',53),(NULL,'Sony','Playstation 3 sa dva dzojstika',54),(NULL,'Sony','Playstation 4 sa FIFA18',55),(NULL,'Sony','Playstation 3',56),(NULL,'Sony','Playstation Vita',57),(NULL,'Nintendo','WII',58),(NULL,'Sony','Playstation 2 sa 10 igrica',59),(NULL,'Sony','Playstation 4 sa 4 dzojstika',60);";
            runQuery(q10);
            databaseConnection.Close();

            string q11 = "INSERT INTO `racunari`(`id`, `cpu`, `maticnaPloca`, `gpu`, `ram`, `hdd`, `zvucnaKartica`, `napajanje`, `idProizvoda`) VALUES (NULL,'Inter core i3','Gigabyte','Intel HD graphics 510','4GB','250 GB HDD','Integrated','500 W',61),(NULL,'Inter core i3','Gigabyte','Intel HD graphics 520','6GB','250 GB HDD','Integrated','500 W',62),(NULL,'Inter core i3','ASUS','Intel HD graphics 530','4GB','500 GB HDD','Integrated','500 W',63),(NULL,'Inter core i3','MSI','Intel HD graphics 540','8GB','500 GB HDD','Integrated','500 W',64),(NULL,'Inter core i5','Gigabyte','Intel HD graphics 560','4GB','250 GB HDD','Integrated','500 W',65),(NULL,'Inter core i5','Gigabyte','Intel HD graphics 800','4GB','250 GB HDD','Integrated','500 W',66),(NULL,'Inter core i5','ASUS','AMD Radeon RX 590','4GB','500 GB HDD','Integrated','500 W',67),(NULL,'Inter core i5','MSI','AMD Radeon RX 760','8GB','500 GB HDD','Integrated','500 W',68),(NULL,'Inter core i5','Gigabyte','Intel HD graphics','8GB','1 TB HDD','Integrated','500 W',69),(NULL,'Inter core i7','Gigabyte','Intel HD graphics','16GB','250 GB SSD','Integrated','500 W',70),(NULL,'Inter core i7','Gigabyte','Intel HD graphics','16GB','250 GB SSD','Integrated','500 W',71),(NULL,'Inter core i7','ASUS','ASUS Dual GeForce - DUAL-RTX2080TI','32GB','250 GB SSD','Integrated','500 W',72),(NULL,'Inter core i7','MSI','ASUS Dual GeForce - DUAL-RTX2080TI','16GB','1 TB HDD','Integrated','500 W',73),(NULL,'Inter core i7','Gigabyte','ASUS GeForce GTX 1050Ti 4GB GDDR5 128bit','32GB','500 GB SSD','Integrated','500 W',74),(NULL,'Inter core i7','Gigabyte','ASUS GeForce GTX 1050Ti 8GB GDDR5 128bit','32GB','600 GB SSD','Integrated','500 W',75);";
            runQuery(q11);
            databaseConnection.Close();

            string q12 = "INSERT INTO `narudzbine`(`id`, `idKupca`, `idProdavca`, `idAlbuma`, `idFilma`, `idIgrice`, `idKonzole`, `idRacunara`, `iznos`, `kolicina`, `datum`) VALUES(NULL,1,1,1,NULL,NULL,NULL,NULL,(SELECT cena FROM albumi a JOIN proizvodi p ON a.idProizvoda = p.id WHERE a.id = 1)*2,2,'2019-09-01'),(NULL,2,1,NULL,NULL,NULL,NULL,4,(SELECT cena FROM racunari r JOIN proizvodi p ON r.idProizvoda = p.id WHERE r.id = 4)*3,3,'2019-09-02'),(NULL,3,1,NULL,12,NULL,NULL,NULL,(SELECT cena FROM filmovi f JOIN proizvodi p ON f.idProizvoda = p.id WHERE f.id = 12)*1,DEFAULT,'2019-09-03'),(NULL,3,2,NULL,NULL,NULL,5,NULL,(SELECT cena FROM konzole k JOIN proizvodi p ON k.idProizvoda = p.id WHERE k.id = 5)*1,DEFAULT,'2019-09-04'),(NULL,4,3,13,NULL,NULL,NULL,NULL,(SELECT cena FROM albumi a JOIN proizvodi p ON a.idProizvoda = p.id WHERE a.id = 13)*1,DEFAULT,'2019-09-05'),(NULL,5,4,NULL,NULL,NULL,NULL,14,(SELECT cena FROM racunari r JOIN proizvodi p ON r.idProizvoda = p.id WHERE r.id = 14)*1,DEFAULT,'2019-09-05'),(NULL,6,5,NULL,NULL,NULL,NULL,7,(SELECT cena FROM racunari r JOIN proizvodi p ON r.idProizvoda = p.id WHERE r.id = 7)*20,20,'2019-09-05'),(NULL,7,6,NULL,NULL,NULL,8,NULL,(SELECT cena FROM konzole k JOIN proizvodi p ON k.idProizvoda = p.id WHERE k.id = 8)*1,DEFAULT,'2019-09-06'),(NULL,8,7,NULL,NULL,4,NULL,NULL,(SELECT cena FROM igrice i JOIN proizvodi p ON i.idProizvoda = p.id WHERE i.id = 4)*5,5,'2019-09-07'),(NULL,1,8,NULL,NULL,9,NULL,NULL,(SELECT cena FROM igrice i JOIN proizvodi p ON i.idProizvoda = p.id WHERE i.id = 9)*3,3,'2019-09-07'),(NULL,1,9,15,NULL,NULL,NULL,NULL,(SELECT cena FROM albumi a JOIN proizvodi p ON a.idProizvoda = p.id WHERE a.id = 15)*1,DEFAULT,'2019-09-08'),(NULL,9,10,NULL,NULL,NULL,4,NULL,(SELECT cena FROM konzole k JOIN proizvodi p ON k.idProizvoda = p.id WHERE k.id = 4)*3,3,'2019-09-09'),(NULL,1,10,NULL,NULL,NULL,NULL,1,(SELECT cena FROM racunari r JOIN proizvodi p ON r.idProizvoda = p.id WHERE r.id = 1)*8,8,'2019-09-09'),(NULL,11,11,NULL,NULL,NULL,NULL,2,(SELECT cena FROM racunari r JOIN proizvodi p ON r.idProizvoda = p.id WHERE r.id = 2)*89,89,'2019-09-10'),(NULL,12,12,NULL,4,NULL,NULL,NULL,(SELECT cena FROM filmovi f JOIN proizvodi p ON f.idProizvoda = p.id WHERE f.id = 4)*150,150,'2019-09-10'),(NULL,13,1,NULL,NULL,2,NULL,NULL,(SELECT cena FROM igrice i JOIN proizvodi p ON i.idProizvoda = p.id WHERE i.id = 2)*53,53,'2019-09-11'),(NULL,14,15,NULL,NULL,14,NULL,NULL,(SELECT cena FROM igrice i JOIN proizvodi p ON i.idProizvoda = p.id WHERE i.id = 14)*5,5,'2019-09-12'),(NULL,15,15,NULL,NULL,NULL,NULL,10,(SELECT cena FROM racunari r JOIN proizvodi p ON r.idProizvoda = p.id WHERE r.id = 10)*12,12,'2019-09-13'),(NULL,12,2,10,NULL,NULL,NULL,NULL,(SELECT cena FROM albumi a JOIN proizvodi p ON a.idProizvoda = p.id WHERE a.id = 10)*2,2,'2019-09-14'),(NULL,7,2,NULL,NULL,NULL,13,NULL,(SELECT cena FROM konzole k JOIN proizvodi p ON k.idProizvoda = p.id WHERE k.id = 13)*1,DEFAULT,'2019-09-14'),(NULL,5,2,NULL,9,NULL,NULL,NULL,(SELECT cena FROM filmovi f JOIN proizvodi p ON f.idProizvoda = p.id WHERE f.id = 9)*1,DEFAULT,'2019-09-16'),(NULL,5,13,NULL,NULL,NULL,NULL,9,(SELECT cena FROM racunari r JOIN proizvodi p ON r.idProizvoda = p.id WHERE r.id = 9)*4,4,'2019-09-16'),(NULL,5,9,NULL,NULL,8,NULL,NULL,(SELECT cena FROM igrice i JOIN proizvodi p ON i.idProizvoda = p.id WHERE i.id = 8)*1,DEFAULT,'2019-09-16'),(NULL,9,15,3,NULL,NULL,NULL,NULL,(SELECT cena FROM albumi a JOIN proizvodi p ON a.idProizvoda = p.id WHERE a.id = 3)*17,17,'2019-09-17'),(NULL,10,15,NULL,NULL,NULL,12,NULL,(SELECT cena FROM konzole k JOIN proizvodi p ON k.idProizvoda = p.id WHERE k.id = 12)*34,34,'2019-09-18'),(NULL,16,16,NULL,NULL,8,NULL,NULL,(SELECT cena FROM igrice i JOIN proizvodi p ON i.idProizvoda = p.id WHERE i.id = 8)*2,2,'2019-08-23'),(NULL,17,17,NULL,NULL,NULL,NULL,6,(SELECT cena FROM racunari r JOIN proizvodi p ON r.idProizvoda = p.id WHERE r.id = 6)*20,20,'2019-08-02'),(NULL,18,18,NULL,13,NULL,NULL,NULL,(SELECT cena FROM filmovi f JOIN proizvodi p ON f.idProizvoda = p.id WHERE f.id = 13)*1,DEFAULT,'2019-09-01'),(NULL,19,19,NULL,NULL,NULL,6,NULL,(SELECT cena FROM konzole k JOIN proizvodi p ON k.idProizvoda = p.id WHERE k.id = 6)*14,14,'2019-09-04'),(NULL,20,20,13,NULL,NULL,NULL,NULL,(SELECT cena FROM albumi a JOIN proizvodi p ON a.idProizvoda = p.id WHERE a.id = 13)*1,DEFAULT,'2019-09-05');";
            runQuery(q12);
            databaseConnection.Close();

      

            MessageBox.Show("Success");

        }

        public void runQuery(string query1)
        {

            MySqlCommand commandDatabase = new MySqlCommand(query1, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            databaseConnection.Open();

            myReader = commandDatabase.ExecuteReader();
        }

        private string GetResult(MySqlDataReader rd)
        {
            
            if (rd.HasRows)
            {
                MessageBox.Show("Your query generated results");
                StringBuilder rezultat = new StringBuilder();

                while (rd.Read())
                {
                    for (int i = 0; i < myReader.FieldCount; i++)
                    {
                        try
                        {
                            rezultat.Append(myReader.GetString(i));
                            rezultat.Append(" ");
                        }
                        catch (System.Data.SqlTypes.SqlNullValueException)
                        {
                            rezultat.Append("NULL");
                            rezultat.Append(" ");
                        }
                        

                    }
                    rezultat.Append("\n");

                }
                return rezultat.ToString();
            }
            
            
            
            return null;
        }


        static string q1 = "SELECT o.ime, o.prezime, COUNT(p.id) AS broj, pos.naziv, os.ime, os.prezime FROM narudzbine n JOIN prodavci p ON n.idProdavca = p.id JOIN poslovnice pos ON p.idPoslovnice = pos.id JOIN menadzeri m ON pos.idMenadzera = m.id JOIN osobe o ON p.idOsobe = o.id JOIN osobe os ON m.idOsobe = os.id GROUP BY p.id ORDER BY broj DESC LIMIT 3;";
        static string q2 = "SELECT o.ime, o.prezime, COUNT(k.id) AS broj FROM narudzbine n JOIN kupci k ON n.idKupca = k.id JOIN osobe o ON k.idOsobe = o.id GROUP BY k.id HAVING broj >= 3;";
        static string q3 = "SELECT COUNT(idAlbuma) AS brojNarucenihAlbuma, COUNT(idFilma) AS brojNarucenihFilmova, COUNT(idIgrice) AS brojNarucenihIgrica, COUNT(idKonzole) AS brojNarucenihKonzola, COUNT(idRacunara) AS brojNarucenihRacunara FROM narudzbine;";
        static string q4 = "SELECT r.id, r.cpu, r.maticnaPloca, r.gpu, r.ram, r.hdd, r.zvucnaKartica, r.napajanje, p.preostalo, p.cena, p.ukupnaZarada FROM racunari r JOIN proizvodi p ON r.idProizvoda = p.id WHERE p.preostalo = 0;";
        static string q5 = "SELECT o.ime, o.prezime, COUNT(o.id) AS broj FROM narudzbine n JOIN prodavci p ON n.idProdavca = p.id JOIN poslovnice pos ON p.idPoslovnice = pos.id JOIN menadzeri m ON pos.idMenadzera = m.id JOIN osobe o ON m.idOsobe = o.id GROUP BY o.id ORDER BY broj ASC LIMIT 1;";
        static string q6 = "SELECT pos.naziv, COUNT(pos.id) AS brojAktivnihPorudzbina FROM narudzbine n JOIN prodavci p ON n.idProdavca = p.id JOIN poslovnice pos ON p.idPoslovnice = pos.id GROUP BY pos.id ORDER BY brojAktivnihPorudzbina DESC LIMIT 2;";
        static string q7 = "SELECT pos.naziv, o.ime, o.prezime, SUM(n.iznos) AS ocekivanaZarada FROM narudzbine n JOIN prodavci p ON n.idProdavca = p.id JOIN poslovnice pos ON p.idPoslovnice = pos.id JOIN menadzeri m ON pos.idMenadzera = m.id JOIN osobe o ON m.idOsobe = o.id GROUP BY pos.id HAVING ocekivanaZarada < 500000;";
        static string q8 = "SELECT i.naziv, i.platforma, p.cena, p.preostalo FROM igrice i JOIN proizvodi p ON i.idProizvoda = p.id ORDER BY p.preostalo DESC LIMIT 3;";
        static string q9 = "SELECT SUM(iznos)/120 AS ocekivaniPrihodUEvrima, ((SELECT COUNT(*) FROM prodavci)*p.plata*12)/120 AS godisnjaPlataRadnikaUEvrima, ((SELECT COUNT(*) FROM menadzeri)*m.plata*12)/120 AS godisnjaPlataMenadzeraUEvrima FROM narudzbine n JOIN prodavci p ON n.idProdavca = p.id JOIN poslovnice pos ON p.idPoslovnice JOIN menadzeri m ON pos.idMenadzera = m.id;";
        static string q10 = "SELECT n.id AS id_narudzbine, o.ime, o.prezime FROM narudzbine n JOIN kupci k ON n.idKupca = k.id JOIN osobe o ON k.idOsobe = o.id WHERE n.datum < '2019-09-05';";
        static string q11 = "SELECT COUNT(n.id) AS brojNarudzbina, k.grad FROM narudzbine n JOIN kupci k ON n.idKupca = k.id GROUP BY k.grad HAVING k.grad != 'Novi Sad' AND k.grad != 'Beograd' AND k.grad != 'Nis' AND k.grad != 'Nis' AND k.grad != 'Kragujevac' ORDER BY  brojNarudzbina DESC LIMIT 1;";
        static string q12 = "SELECT k.drzava, COUNT(n.id) AS brojNarudzbina FROM narudzbine n JOIN kupci k ON n.idKupca = k.id GROUP BY k.drzava HAVING k.drzava != 'Srbija' ORDER BY brojNarudzbina DESC LIMIT 1;";
        static string q13 = "SELECT pos.naziv, SUM(n.iznos) AS ocekivanaZarada FROM narudzbine n JOIN prodavci p ON n.idProdavca = p.id JOIN poslovnice pos ON p.idPoslovnice = pos.id GROUP BY pos.id ORDER BY ocekivanaZarada DESC LIMIT 1;";
        static string q14 = "SELECT SUM(n.iznos), k.grad FROM narudzbine n JOIN kupci k ON n.idKupca = k.id GROUP BY k.grad  ORDER BY SUM(n.iznos) DESC LIMIT 1;";
        static string q15 = "SELECT n.id AS id_narudzbine, o.ime, o.prezime, o.email, n.iznos, pos.naziv FROM narudzbine n JOIN kupci k ON n.idKupca = k.id JOIN osobe o ON k.idOsobe = o.id JOIN prodavci p ON n.idProdavca = p.id JOIN poslovnice pos ON p.idPoslovnice = pos.id WHERE n.idKonzole IN (SELECT id FROM konzole) ORDER BY n.iznos DESC LIMIT 3;";
        static string q16 = "SELECT o.ime, o.prezime, o.email FROM kupci k JOIN osobe o ON k.idOsobe = o.id WHERE k.id NOT IN (SELECT idKupca FROM narudzbine) AND k.ukupanTrosak > 50000;";
        static string q17 = "SELECT COUNT(*) AS brojNarucenih, a.zanr FROM albumi a JOIN proizvodi p ON a.idProizvoda = p.id GROUP BY a.zanr ORDER BY brojNarucenih DESC LIMIT 3;";
        static string q18 = "SELECT n.id, n.iznos, (n.iznos-(n.iznos/10)) AS iznosSaPopustom FROM narudzbine n JOIN prodavci p ON n.idProdavca = p.id WHERE n.idFilma IS NOT NULL;";

        public void button1_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            switch (index)
            {
                case 0:
                    runQuery(q1);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 1:
                    runQuery(q2);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 2:
                    
                    runQuery(q3);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 3:
                   
                    runQuery(q4);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 4:
                    
                    runQuery(q5); 
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 5:
                    
                    runQuery(q6);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 6:
                    
                    runQuery(q7);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 7:
                    
                    runQuery(q8);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 8:

                    runQuery(q9);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 9:
                   
                    runQuery(q10);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 10:
                    
                    runQuery(q11);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 11:
                    
                    runQuery(q12);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 12:
                    
                    runQuery(q13);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 13:
                    
                    runQuery(q14);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 14:
                    runQuery(q15);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 15:
                    runQuery(q16);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 16:
                    runQuery(q17);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;
                case 17:
                    runQuery(q18);
                    addToTb(GetResult(myReader));
                    databaseConnection.Close();
                    break;



            }
            
        }
       
        private void addToTb(string text)
        {
            textBox1.Text = text;
            textBox1.Text = text.Replace("\n", Environment.NewLine);

        }

        private void appendToTb(string text)
        {
            
            textBox1.Text += text + Environment.NewLine + Environment.NewLine;
            //textBox1.Text = text.Replace("\n", Environment.NewLine);
            




        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        public static string dtfordb(DateTime dt)
        {
            return dt.Year + "-" + dt.Month + "-" + dt.Day + " " + dt.TimeOfDay;
        }


        
        
        private void onloadCb()
        {
    
            comboBox1.Items.Add("1. Imena i prezimena prodavaca koji imaju najvise neisporucenih narudzbina i menadzera njihovih poslovnica");
            comboBox1.Items.Add("2. Imena i prezimena kupaca koji imaju 3 ili vise neisporucenih narudzbina");
            comboBox1.Items.Add("3. Koliko je koji proizvod narucivan?");
            comboBox1.Items.Add("4. Racunari(najvise narucivan proizvod) kojih nema vise na lageru");
            comboBox1.Items.Add("5. Ime i prezime menadzera ciji prodavci imaju najmanje narudzbina");
            comboBox1.Items.Add("6. Dve poslovnice sa najvise narudzbina");
            comboBox1.Items.Add("7. Koje poslovnice ocekuju manje od 500.000 dinara prihoda od neisporucenih narudzbina?");
            comboBox1.Items.Add("8. Nazive tri igrice kojih je ostalo najvise na lageru");
            comboBox1.Items.Add("9. Kolika je prosecna ocekivana zarada od neisporucenih narudzbina i da li ima dovoljno sredstava od istih za isplatu svog osoblja za 2019 godinu?");
            comboBox1.Items.Add("10. Da li ima i koje su narudzbine narucene pre vise od dve nedelje?");
            comboBox1.Items.Add("11. Grad iz kojeg ima najvise narudzbina, a u tom gradu ne postoji poslovnica");
            comboBox1.Items.Add("12. Iz koje drzave u regionu(osim Srbije) ima najvise naruzbina");
            comboBox1.Items.Add("13. Koja poslovnica ocekuje najveci prihod od neisporucenih narudzbina?");
            comboBox1.Items.Add("14. Ispisuje iz kog grada ce kupci najvise potrositi na narudzbine");
            comboBox1.Items.Add("15. Imena i prezimena tri kupca koja imaju najvrednije narudzbine konzola i nazivi poslovnica iz kojih su proizvodi naruceni");
            comboBox1.Items.Add("16. Imena, prezimena i email kupaca u bazi koji nemaju nijednu aktivnu narudzbinu a potrosili su vise od 50.000 dinara do sada");
            comboBox1.Items.Add("17. Tri najvise narucivana zanra muzike");
            comboBox1.Items.Add("18. Cene narucenih filmova sa popustom od 10%");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to drop all the tables?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            switch (result)
            {
                case DialogResult.Yes:
                    string drop = "DROP DATABASE gaming_center;";
                    MessageBox.Show("Success");
                    runQuery(drop);
                    databaseConnection.Close();
                    break;

            }
            
        }
       
        private void button3_Click(object sender, EventArgs e)
        {
            fillTable();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            createTables();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
