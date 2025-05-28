# ✨ SignalRDev - Gerçek Zamanlı Mesajlaşma ve Grup Sohbet Uygulaması 🌟

![Chat Icon](https://img.icons8.com/plasticine/100/000000/chat.png)

SignalRDev'e hoş geldiniz! Anlık iletişimin gücünü sonuna kadar hissedeceğiniz, benzersiz bir gerçek zamanlı mesajlaşma deneyimi sunan mobil ve web uygulamamızla tanışın! 🚀

Bu proje, ASP.NET Core MVC ve SignalR'ın muhteşem uyumunu kullanarak geliştirilmiş, birebir ve grup mesajlaşma özelliklerine sahip bir web uygulamasıdır. Amacımız, kullanıcıların kolayca bağlanıp, güvenli ve hızlı bir şekilde iletişim kurabilmelerini sağlamaktır. İster özel sohbetler, ister dinamik grup tartışmaları, SignalRDev hepsini kapsar! 🔥

---

## 📱 Özellikler

* **⚡ Gerçek Zamanlı Mesajlaşma:** SignalR ile mesajlarınız anında alıcısına ulaşır. Gecikme derdi yok!
    ![Real-time Icon](https://img.icons8.com/nolan/96/real-time.png)

* **🗣️ Birebir Mesajlaşma:** Sistemdeki diğer kullanıcılarla güvenli ve özel birebir sohbetler yapın.
    ![Private Chat Icon](https://img.icons8.com/nolan/96/user.png)

* **👥 Grup Oluşturma ve Yönetimi:** Kendi gruplarınızı kurun, üye ekleyin veya ayrılın. Grup yöneticileri (adminler) üyeleri çıkarabilir veya başkalarını admin yapabilir.
    ![Group Chat Icon](https://img.icons8.com/nolan/96/user-groups.png)

* **💬 Grup Sohbeti:** Gruplarınızda anlık toplu mesajlaşmanın tadını çıkarın. Tüm grup üyeleri mesajlarınıza anında erişir.
    ![Chat Bubble Icon](https://img.icons8.com/nolan/96/chat-bubble.png)

* **🟢 Online Kullanıcı Takibi:** Kimlerin online olduğunu gerçek zamanlı görün. Online kullanıcılar yeşil bir simgeyle gösterilir.
    ![Online Status Icon](https://img.icons8.com/nolan/96/online-user.png)

* **🔒 Kullanıcı Kimlik Doğrulama:** ASP.NET Identity ile kullanıcı kaydı, girişi ve kimlik doğrulama işlemleri güvenli bir şekilde yapılır.
    ![Security Icon](https://img.icons8.com/nolan/96/lock--v1.png)

* **🎨 Kullanıcı Arayüzü:** Modern ve kullanıcı dostu arayüzümüzle mesajlaşmak, grupları ve kullanıcıları yönetmek çok kolay!
    ![UI Icon](https://img.icons8.com/nolan/96/monitor.png)

---

## ⚡️ Teknolojiler Used

* **ASP.NET Core MVC** - Sağlam ve ölçeklenebilir bir web uygulaması çatısı.
* **SignalR** - Gerçek zamanlı web fonksiyonelliği için tasarlanmış harika bir kütüphane.
* **Entity Framework Core** - Veritabanı etkileşimleri için güçlü bir ORM.
* **ASP.NET Identity** - Güvenli kullanıcı yönetimi çözümü.
* **SQL Server** - Verilerinizin emin ellerde olduğu sağlam bir veritabanı.
* **Bootstrap** - Hızlı ve duyarlı UI geliştirme için olmazsa olmaz.

---

## 🏗️ Proje Mimarisi

* **Controllers:** Kullanıcı, mesaj ve grup işlemlerini yöneten ana akış noktaları.
* **Models:** Kullanıcı, mesaj, grup ve grup üyeliği gibi temel veri modelleri.
* **Services:** Mesajlaşma, grup yönetimi ve online kullanıcı takibi için iş mantığını barındıran servis katmanı.
* **Views:** Razor tabanlı, kullanıcı dostu arayüz sayfaları.
* **Data:** Entity Framework Core ile veritabanı işlemleri ve konfigürasyonları.

---

## ⚡️ Getting Started

Uygulamayı ayağa kaldırmak oldukça kolaydır. Sadece birkaç adımı takip edin:

1.  **Bağımlılıkları yükleyin:**
    ```bash
    dotnet restore
    ```

2.  **Veritabanı bağlantı ayarlarını yapın:**
    `appsettings.json` dosyasındaki `DefaultConnection` kısmını kendi veritabanınıza göre düzenleyin.

3.  **Veritabanını güncelleyin (Eğer migration atmak isterseniz):**
    ```bash
    dotnet ef database update
    ```

4.  **Projeyi başlatın:**
    ```bash
    dotnet run
    ```

5.  **Uygulamayı tarayıcıda açın:**
    ```
    https://localhost:5001
    ```

---

 
 
## 📜 Lisans

Bu proje, **MIT Lisansı** ile lisanslanmıştır. Daha fazla bilgi için `LICENSE` dosyasına göz atın.
