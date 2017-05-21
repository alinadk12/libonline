using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibOnline.Models
{
    public class LibOnlineDbInitializer : DropCreateDatabaseAlways<LibOnlineContext>
    {
        protected override void Seed(LibOnlineContext db)
        {

            //управление ролями
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            
            //создание ролей пользователей
            var adminRole = new IdentityRole { Name = "Администратор" };
            var userRole = new IdentityRole { Name = "Пользователь" };

            //добавление ролей в БД
            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            //управление пользователями
            var userManager = new UserManager<LibOnlineUser>(new UserStore<LibOnlineUser>(db));

            //создание админа
            var admin = new LibOnlineUser { UserName = "admin@admin.com", Name = "Администратор", Surname = "Сайта", Email = "admin@admin.com" };
            string password = "admin123";
            var result = userManager.Create(admin, password);

            //если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                //добавляем для пользователя роль
                userManager.AddToRole(admin.Id, adminRole.Name);
            }

            //создаем обычного пользователя
            var user = new LibOnlineUser { UserName = "user@user.com", Name = "Обычный", Surname = "Пользователь" };
            password = "password";
            result = userManager.Create(user, password);

            //если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                //добавляем для пользователя роль
                userManager.AddToRole(user.Id, userRole.Name);
            }

            //----------------------добавление жанров---------------------
            Genre g1 = new Genre { Name = "Религия", Description = "Религиозная литература обо всех мировых направлениях религии, священные писания, религоведение." };
            db.Genres.Add(g1);
            Genre g2 = new Genre { Name = "Фантастика и Фэнтези", Description = "Жанр предполагает то, чего не может быть. Либо это современность, наполненная инопланетными гостями, либо гостями из будущего. Либо события происходят на других планетах, в далеком прошлом или далеком будущем. Есть варианты исторической фантастики, т.е. автор описывает какие-то исторические события, которые могли бы произойти иначе, если бы… допустим, Наполеон одержал победу." };
            db.Genres.Add(g2);
            Genre g3 = new Genre { Name = "Детективы и триллеры", Description = "Жанр подразумевает преступление, расследование и развязку, т.е. читатель узнает кто и как совершил преступление. Иногда преступников даже наказывают. " };
            db.Genres.Add(g3);
            Genre g4 = new Genre { Name = "Поэзия и драматургия", Description = "Бывают драмы, мелодрамы и комедии. Отличительной чертой жанра выступает предположительность действий на сцене." };
            db.Genres.Add(g4);
            Genre g5 = new Genre { Name = "Приключения", Description = "Жанр предполагает непременную интригу, различного рода передряги в пути главных героев. Неожиданные повороты судьбы и, наконец, кульминация. Например, алмазные подвески вопреки всему оказались доставленными в Париж! А счастливый Д`Артаньян смог доказать свою любовь Констанции Бонасье." };
            db.Genres.Add(g5);
            Genre g6 = new Genre { Name = "Сказки", Description = "Жанр, безгранично богатый на фантазии. Волшебство, интриги, удивительные вещи, обладающие колдовскими свойствами, странные существа…обязательны." };
            db.Genres.Add(g6);
            Genre g7 = new Genre { Name = "Компьютеры и Интернет", Description = "" };
            db.Genres.Add(g7);
            Genre g8 = new Genre { Name = "Романы", Description = "Много проблемное произведение, изображающее человека в процессе его становления и развития. Действие в романе насыщено внешними или внутренними конфликтами. По тематике бывают: исторические, сатирические, фантастические, философские и др. По структуре: роман в стихах, эпистолярный роман и др." };
            db.Genres.Add(g8);
            Genre g9 = new Genre { Name = "Наука, Образование", Description = "" };
            db.Genres.Add(g9);
            Genre g10 = new Genre { Name = "Справочники", Description = "" };
            db.Genres.Add(g10);
            Genre g11 = new Genre { Name = "Повести, рассказы", Description = "Эпическое произведение средней или большой формы, построенное в виде повествования о событиях в их естественной последовательности. В отличие от романа в повести материал излагается хроникально, нет острого сюжета, нет голубого анализа чувств героев. В повести не ставятся задачи глобального исторического характера." };
            db.Genres.Add(g11);
            Genre g12 = new Genre { Name = "Классическая проза", Description = "" };
            db.Genres.Add(g12);
            Genre g13 = new Genre { Name = "Деловая литература", Description = "" };
            db.Genres.Add(g13);
            //-------------------------------------------------------------------

            //----------------------добавление стран-----------------------------
            Country AU = new Country { Name = "Австралия", CountryId = "AU" };
            db.Countries.Add(AU);
            Country AT = new Country { Name = "Австрия", CountryId = "AT" };
            db.Countries.Add(AT);
            Country AR = new Country { Name = "Аргентина", CountryId = "AR" };
            db.Countries.Add(AR);
            Country BR = new Country { Name = "Бразилия", CountryId = "BR" };
            db.Countries.Add(BR);
            Country GB = new Country { Name = "Великобритания", CountryId = "GB" };
            db.Countries.Add(GB);
            Country DE = new Country { Name = "Германия", CountryId = "DE" };
            db.Countries.Add(DE);
            Country GR = new Country { Name = "Греция", CountryId = "GR" };
            db.Countries.Add(GR);
            Country ES = new Country { Name = "Испания", CountryId = "ES" };
            db.Countries.Add(ES);
            Country IT = new Country { Name = "Италия", CountryId = "IT" };
            db.Countries.Add(IT);
            Country CN = new Country { Name = "КНР", CountryId = "CN" };
            db.Countries.Add(CN);
            Country NL = new Country { Name = "Нидерланды", CountryId = "NL" };
            db.Countries.Add(NL);
            Country PL = new Country { Name = "Польша", CountryId = "PL" };
            db.Countries.Add(PL);
            Country RU = new Country { Name = "Россия", CountryId = "RU" };
            db.Countries.Add(RU);
            Country US = new Country { Name = "США", CountryId = "US" };
            db.Countries.Add(US);
            Country FR = new Country { Name = "Франция", CountryId = "FR" };
            db.Countries.Add(FR);
            Country JP = new Country { Name = "Япония", CountryId = "JP" };
            db.Countries.Add(JP);
            //-------------------------------------------------------------------------

            //---------------------добавление издательств------------------------------
            PublishingHouse ph1 = new PublishingHouse { Name = "РОСМЭН" };
            db.PublishingHouses.Add(ph1);
            PublishingHouse ph2 = new PublishingHouse { Name = "Азбука" };
            db.PublishingHouses.Add(ph2);
            PublishingHouse ph3 = new PublishingHouse { Name = "ЭКСМО" };
            db.PublishingHouses.Add(ph3);
            PublishingHouse ph4 = new PublishingHouse { Name = "Питер Принт" };
            db.PublishingHouses.Add(ph4);
            PublishingHouse ph5 = new PublishingHouse { Name = "Питер" };
            db.PublishingHouses.Add(ph5);
            PublishingHouse ph6 = new PublishingHouse { Name = "Новое знание" };
            db.PublishingHouses.Add(ph6);
            PublishingHouse ph7 = new PublishingHouse { Name = "Амфора" };
            db.PublishingHouses.Add(ph7);
            PublishingHouse ph8 = new PublishingHouse { Name = "Наука и техника" };
            db.PublishingHouses.Add(ph8);
            PublishingHouse ph9 = new PublishingHouse { Name = "Символ-Плюс" };
            db.PublishingHouses.Add(ph9);
            PublishingHouse ph10 = new PublishingHouse { Name = "ТУСУР" };
            db.PublishingHouses.Add(ph10);
            PublishingHouse ph11 = new PublishingHouse { Name = "Вильямс" };
            db.PublishingHouses.Add(ph11);
            PublishingHouse ph12 = new PublishingHouse { Name = "Форум" };
            db.PublishingHouses.Add(ph12);
            PublishingHouse ph13 = new PublishingHouse { Name = "АСТ, Астрель" };
            db.PublishingHouses.Add(ph13);
            PublishingHouse ph14 = new PublishingHouse { Name = "Альфа-Книга" };
            db.PublishingHouses.Add(ph14);
            //-----------------------------------------------------------------------------

            //-------------------------добавляем авторов-----------------------------------
            Author JV = new Author { Name = "Жюль", Surname = "Верн", Birthday = new DateTime(1828, 02, 08), Obit = new DateTime(1905, 03, 24), Country = FR, Biography = "Французский географ и писатель, классик приключенческой литературы, один из основоположников жанра научной фантастики. Член Французского Географического общества. " };
            db.Authors.Add(JV);
            Author EMR = new Author { Name = "Эрих", Patronymic = "Мария", Surname = "Ремарк", Birthday = new DateTime(1898, 06, 22), Obit = new DateTime(1970, 09, 25), Country = DE, Biography = "Видный немецкий писатель XX века, представитель потерянного поколения. Его роман «На Западном фронте без перемен» входит в большую тройку романов «потерянного поколения», изданных в 1929 году, наряду с работами «Прощай, оружие!» Эрнеста Хемингуэя и «Смерть героя» Ричарда Олдингтона." };
            db.Authors.Add(EMR);
            Author MAB = new Author { Name = "Михаил", Patronymic = "Афанасьевич", Surname = "Булгаков", Birthday = new DateTime(1891, 05, 15), Obit = new DateTime(1940, 03, 10), Country = RU, Biography = "Русский писатель, драматург, театральный режиссёр и актёр. Автор повестей и рассказов, множества фельетонов, пьес, инсценировок, киносценариев, оперных либретто." };
            db.Authors.Add(MAB);
            Author SK = new Author { Name = "Стивен", Surname = "Кинг", Birthday = new DateTime(1947, 09, 21), Country = US, Biography = "Американский писатель, работающий в разнообразных жанрах, включая ужасы, триллер, фантастику, фэнтези, мистику, драму; получил прозвище «Король ужасов»." };
            db.Authors.Add(SK);
            Author CP = new Author { Name = "Кристофер", Surname = "Паолини", Birthday = new DateTime(1983, 11, 17), Country = US, Biography = "Американский писатель, автор тетралогии в стиле фэнтези Эрагон." };
            db.Authors.Add(CP);
            Author JR = new Author { Name = "Джоан", Surname = "Роулинг", Birthday = new DateTime(1965, 07, 31), Country = GB, Biography = "Британская писательница, сценарист и кинопродюсер, наиболее известная как автор серии романов о Гарри Поттере." };
            db.Authors.Add(JR);
            Author DAG = new Author { Name = "Дмитрий", Patronymic = "Алексеевич", Surname = "Глуховский", Birthday = new DateTime(1979, 06, 11), Country = RU, Biography = "Российский писатель и журналист, автор романов-антиутопий «Метро 2033», «Метро 2034», «Метро 2035», «Будущее», «Сумерки», «Рассказы о Родине». Создатель книжной серии «Вселенная Метро 2033». Военный корреспондент, радиоведущий. Совершил первый в мире прямой телерепортаж из Северного Полюса в июле 2007 года." };
            db.Authors.Add(DAG);
            //------------------------------------------------------------------------------

            //---------------------------добавляем книги------------------------------------
            Book b1 = new Book { Title = "Вокруг света за 80 дней", YearOfPublishing = 2006, NumberOfPage = 192, Genre = new List<Genre>() { g5 }, Author = new List<Author>() { JV }, PublishingHouse = ph13, Description = "В \"Вокруг света за 80 дней\" Верн описывает невозмутимого англичанина и его расторопного слугу, которые на спор спешат как можно скорее обогнуть земной шар, испытывая массу приключений. В отличие от многих других вымышленных путешествий в книгах Верна, совершавшихся на фантастических, еще не изобретенных средствах транспорта, здесь герои использовали уже существовавшие средства." };
            db.Books.Add(b1);
            Book b2 = new Book { Title = "Двадцать тысяч лье под водой", YearOfPublishing = 2008, NumberOfPage = 480, Genre = new List<Genre>() { g2, g5 }, Author = new List<Author>() { JV }, PublishingHouse = ph13, Description = "История профессора Пьера Аронакса и его друзей, волей случая оказавшихся на подводном корабле таинственного капитана Немо..." };
            db.Books.Add(b2);
            Book b3 = new Book { Title = "Три товарища", YearOfPublishing = 2000, NumberOfPage = 448, Genre = new List<Genre>() { g12 }, Author = new List<Author>() { EMR }, PublishingHouse = ph2, Description = "Трагедия Первой и Второй мировой, боль \"потерянного поколения\", попытка создать для себя во \"времени, вывихнувшим сустав\" забавный, в чем-то циничный, а в чем-то щемяще-чистый маленький мир верной дружбы и отчаянной любви - таков Ремарк, чья проза не принадлежит старению..." };
            db.Books.Add(b3);
            Book b4 = new Book { Title = "Мастер и Маргарита", YearOfPublishing = 2006, NumberOfPage = 415, Genre = new List<Genre>() { g12 }, Author = new List<Author>() { MAB }, PublishingHouse = ph3, Description = "В \"Мастере и Маргарите\" есть все: веселое озорство и щемящая печаль, романтическая любовь и колдовское наваждение, магическая тайна и безрассудная игра с нечистой силой." };
            db.Books.Add(b4);
            Book b5 = new Book { Title = "11/22/63", YearOfPublishing = 2013, NumberOfPage = 800, Genre = new List<Genre>() { g2 }, Author = new List<Author>() { SK }, PublishingHouse = ph13, Description = "Убийство президента Кеннеди стало самым трагическим событием американской истории XX века. Тайна его до сих пор не раскрыта. Но что, если случится чудо ? Если появится возможность отправиться в прошлое и предотвратить катастрофу? Это предстоит выяснить обычному учителю из маленького городка Джейку Эппингу, получившему доступ к временному порталу. Его цель — спасти Кеннеди. Но какова будет цена спасения ? " };
            db.Books.Add(b5);
            Book b6 = new Book { Title = "Эрагон", YearOfPublishing = 2006, NumberOfPage = 634, Genre = new List<Genre>() { g2 }, Author = new List<Author>() { CP }, PublishingHouse = ph1, Description = "Таинственная находка изменила жизнь Эрагона, обыкновенного мальчика из страны Алагейзия. Чтобы отомстить за близких, он покинет родные места. В поисках Эллесмеры, города эльфов, он попадает в фантастические земли, наполненные красотой и опасностью, сразится с колдунами и чудовищами, спустится к сердцу горы в королество гномов." };
            db.Books.Add(b6);
            Book b7 = new Book { Title = "Метро 2035", YearOfPublishing = 2016, NumberOfPage = 424, Genre = new List<Genre>() { g2, g5 }, Author = new List<Author>() { DAG }, PublishingHouse = ph13, Description = "Третья мировая стерла человечество с лица Земли. Планета опустела. Мегаполисы обращены в прах и пепел. Железные дороги ржавеют. Спутники одиноко болтаются на орбите. Радио молчит на всех частотах. Выжили только те, кто, услышав сирены тревоги, успел добежать до дверей московского метро. Там, на глубине в десятки метров, на станциях и в туннелях, люди пытаются переждать конец света. Там они создали себе новый мирок вместо потерянного огромного мира. Они цепляются за жизнь изо всех сил и отказываются сдаваться. Они мечтают вернуться наверх - однажды, когда радиационный фон от ядерных бомбардировок спадет. И не оставляют надежды найти других выживших... \"Метро 2035\" продолжает - и завершает историю Артема из первой книги культовой трилогии. " };
            db.Books.Add(b7);
            Book b8 = new Book { Title = "Под куполом", YearOfPublishing = 2015, NumberOfPage = 1120, Genre = new List<Genre>() { g2 }, Author = new List<Author>() { SK }, PublishingHouse =  ph13, Description = "История маленького городка, который настигла большая беда. Однажды его, вместе со всеми обитателями, накрыло таинственным невидимым куполом, не позволяющим ни покинуть город, ни попасть туда извне. Что теперь будет в городке? Что произойдет с его жителями? Ведь когда над человеком не довлеет ни закон, ни страх наказания - слишком тонкая грань отделяет его от превращения в жестокого зверя.Кто переступит эту грань, а кто - нет ?" };
            db.Books.Add(b8);
            Book b9 = new Book { Title = "Метро 2033", YearOfPublishing = 2015, NumberOfPage = 384, Genre = new List<Genre>() { g2, g5 }, Author = new List<Author>() { DAG }, PublishingHouse = ph13, Description = "Двадцать лет спустя Третьей мировой войны последние выжившие люди прячутся на станциях и в туннелях московского метро, самого большого на Земле противоатомного бомбоубежища. Поверхность планеты заражена и непригодна для обитания, и станции метро становятся последним пристанищем для человека. Они превращаются в независимые города-государства, которые соперничают и воюют друг с другом. Они не готовы примириться даже перед лицом новой страшной опасности, которая угрожает всем людям окончательным истреблением. Артем, двадцатилетний парень со станции ВДНХ, должен пройти через все метро, чтобы спасти свой единственный дом - и все человечество." };
            db.Books.Add(b9);
            Book b10 = new Book { Title = "Метро 2034", YearOfPublishing = 2015, NumberOfPage = 384, Genre = new List<Genre>() { g2, g5 }, Author = new List<Author>() { DAG }, PublishingHouse = ph13, Description = "2034 год. Мир уничтожен ядерной войной.Жизнь на поверхности Земли больше невозможна.Спаслись только те, кто, услышав сигнал тревоги, успел добежать до дверей московского метро - самого большого в мире бомбоубежища.Два десятилетия спустя станции метро превратились в города - государства, а путь между ними лежит сквозь мрак и опасности туннелей.Цивилизация исчезает.Человек постепенно забывает о том, что делало его человеком.Станция Севастопольская - маленькая подземная Спарта, противостоящая в одиночку ордам нечисти - оказывается отрезана от большого метро и будет неминуемо уничтожена.Чтобы спасти ее, нужен настоящий герой… Или героиня?" };
            db.Books.Add(b10);
            //--------------------------------------------------------------------------------

            base.Seed(db);
        }
    }
}