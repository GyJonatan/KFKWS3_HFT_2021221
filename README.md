A Model osztályokban legyenek letárolva az idegen kulcsok és használjon Navigation Propertyket LazyLoader-rel ahol lehet! 
A Linq lekérdezésekben akkor használjunk join-t, ha elkerülhetetlen;

  • Az mdf és ldf fájlokat a Data rétegben kell létrehozni, ahol be kell állítani a Build Action-t
    Content értékre, a Copy to Output Directory lehetőséget pedig Copy always értékre;

  • Állítson be olyan Connection String értéket, amely a mindenkori munkakönyvtárban lévő lokális
    adatbázis fájlhoz kapcsolódik;

  • A Data rétegben a DbContext osztály OnModelCreating metódusában töltse fel az adatbázist
    minden indításkor tesztadatokkal! A felhasználó tudjon indításkor már meglévő adatokból
    elindulni;

  • A fejlesztés közben lehetőség van arra, hogy a ConsoleApp is megkapja Project Reference-ként
    a Logic, Repository, Data és Models rétegeket. Ezzel tesztelje az alkalmazást működés közben,
    de a projekt végére a ConsoleApp majd csak API hívásokkal kommunikálhat az Endpoint
    réteggel és csak a Models library-t ismerheti!

  • A Logic a Repository-t, mint függőséget csak interfészen át, konstruktor paraméterként
    kaphatja meg (Dependency Injection)! A repository a DbContext függőséget csak konstruktor
    paraméterként kaphatja meg, az Endpoint controller-jei a logic függőséget csak interfészen át,
    konstruktor paraméterként kaphatják meg! A függőségek beszúrását az Endpoint projekt végzi,
    IoC konténer segítségével! Tesztelési célból a konzolos alkalmazásban kézzel példányosíthatóak;

  • Minden Model osztályhoz szükséges elkészíteni 1-1 repository osztályt, amely tartalmazza a
    CRUD metódusokat (Create, Read, ReadAll, Update, Delete). A ReadAll metódus IQueryable<T>
    interfészen át adja vissza a logic-nak a DbSet-eket;

  • A Logic rétegnek szintén biztosítania kell ezeket a CRUD metódusokat, valamint szükséges
    legalább 5 db non-crud metódust készíteni a logic-ban, amelyek több táblás lekérdezést
    használnak! A CRUD és non-crud metódusok eredményeit IEnumerable<T> interfészen keresztül
    adja vissza a felsőbb rétegeknek! Non-crudokra néhány példa: egy adott autó márkára ki az a
    megrendelő, aki a legnagyobb összegben adott le bérlési igényt. (Ehhez a lekérdezéshez pl.
    mindhárom entitásra szükség van);

  • A Test projektben NUnit és Moq könyvtárakat kell használni. A Logic a Moq segítségével egy áladatbázist kap függőségként. 
    A unit tesztek elsősorban a non-crud metódusokat tesztelik a Logic-ból! 
    Valamint a Logic-ban lévő Create metódusok hibakezelését (pl.: névként üres string
    dobjon kivételt, stb.)! Egy Logic-beli Create abban különbözik egy Repository-beli Create-től,
    hogy hibakezelést is végez, Exception-öket dob. A Repository-beli Create ellenőrzés nélkül
    mentse el az adatbázisba a megkapott objektumot;

  • A féléves feladatban minimum 10 db Unit tesztet kell létrehozni! Pl: 5 db non-crud, 3 db create
    és 2 szabadon választott egyéb teszt;

  • Minden Model osztályhoz tartozzon egy Repository osztály (pl: Car à CarRepository) és egy
    Logic osztály (Car à CarLogic). Egy Logic osztály ismerhet és felhasználhat több Repository-is 
    (pl.: CarLogic ismerheti a CarRepository-t és a BrandRepository-t is, ha szükséges a
    lekérdezéshez több repository adata is);

  • A projekt Endpoint rétege ismeri a Logic osztályokat, és a bennük lévő funkciókat publikálja a
    külvilág felé API Endpointok formájában! Minden Logic osztályhoz tartozhat egy vagy több
    ApiController. Az ApiControllerek Action-jei feleltethetőek meg a Logic rétegek metódusainak.
    Célszerűen:

          o HTTP GET --> Read, ReadAll;

          o HTTP POST --> Create;

          o HTTP PUT --> Update;

          o HTTP DELETE --> Delete.

  • A konzolos alkalmazás API kéréseket küld az Endpoint felé JSON üzenetek formájában. A
    konzolos alkalmazásból elérhetőnek kell lennie az összes CRUD metódusnak és összes non-CRUD
    metódusnak! Erre használható a ConsoleMenu-Simple NuGet csomag opcionálisan.
