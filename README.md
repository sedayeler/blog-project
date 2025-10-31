# 📝 Yorum Satırı - ASP.NET Core MVC Blog Sitesi

**Yorum Satırı**, kullanıcıların blog gönderileri oluşturabildiği, yorum yapabildiği ve yapay zeka destekli özetleme özelliği ile içeriklere hızlıca göz atabildiği bir blog platformudur. Bu proje, .NET 8, Onion Architecture ve modern yazılım geliştirme yaklaşımlarıyla geliştirilmiştir.

## 🚀 Kullanılan Teknolojiler

- ASP.NET Core MVC (.NET 8)
- Onion Architecture
- PostgreSQL
- Entity Framework Core
- SOLID Prensipleri
- Dependency Injection & Generic Repository Pattern
- CQRS Pattern (MediatR ile)
- ASP.NET Core Identity
- Cookie Based Authentication
- SMTP (E-posta gönderimi için)
- Gemini API (Yapay zeka ile özetleme)
- Razor Pages
- Bootstrap 5
- LibMan 

## 🔑 Özellikler

- Kullanıcılar kayıt olabilir, giriş/çıkış yapabilir.
- Kayıt olan her kullanıcıya **hoş geldin e-postası** gönderilir.
- Giriş yapan kullanıcılar:
  - Yeni gönderi oluşturabilir.
  - Gönderilere görsel ekleyebilir.
  - Gönderilerini düzenleyip silebilir.  
  - Gönderilere yorum yapabilir, yorumlarını silebilir.
- Misafir kullanıcılar:
  - Gönderileri ve yorumları görüntüleyebilirler.
  - Yeni gönderi oluşturamazlar.
  - Yorum yapamazlar.
- Gönderiler kategorilere göre filtrelenebilir.
- Arama özelliği mevcuttur.
- Yapay zeka ile gönderiler özetlenebilir.

### Ana Sayfa
![Image](https://github.com/user-attachments/assets/52fcb771-1cac-461a-a64b-8eb69ab1eb0e)

### Kayıt Ol Ekranı
![Image](https://github.com/user-attachments/assets/11161460-6a5a-4993-850f-09bffa962d1d)

### Hoş Geldin E-Postası
![Image](https://github.com/user-attachments/assets/19df0848-e914-4372-a4cc-fcee4df8cdde)

### Giriş Ekranı
![Image](https://github.com/user-attachments/assets/d33aaa2e-82af-43c2-b47e-f11414915eba)

### Gönderi Oluşturma Sayfası
![Image](https://github.com/user-attachments/assets/5092fa57-f420-4a0d-a51d-69618fd81ea6)

### Kullanıcının kendi gönderisine ait detay sayfası
![Image](https://github.com/user-attachments/assets/056ce825-4c59-4a0e-bc72-73382e129d4f)

### Yapay Zeka ile Gönderi Özeti
![Image](https://github.com/user-attachments/assets/e8eee134-dd9e-4c1e-ad28-a8747100c3aa)

### Yorumlar Ekranı
![Image](https://github.com/user-attachments/assets/d1858cad-ced2-4ad0-8911-a453cd5e2386)

## Kurulum 

Aşağıdaki adımları takip ederek blog projesini kendi bilgisayarınızda çalıştırabilirsiniz:

### 1. Reponun Klonlanması

```bash
git clone https://github.com/sedayeler/blog-project
cd blog-project
```

### 2. Bağımlılıkların Kurulması

```bash
dotnet restore
```

### 3. Veritabanı Ayarları

- `appsettings.json` dosyasını açın ve PostgreSQL bağlantı cümlesini kendi bilgilerinize göre güncelleyin:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=YorumSatiriDb;Username=postgres;Password=postgres"
}
```

- Ardından veritabanını oluşturmak için migration'ları uygulayın:

```bash
dotnet ef database update
```

### 4. Uygulamayı Çalıştırma

```bash
dotnet run --project BlogProject.WebUI
```

### 5. Uygulamaya Erişim

Uygulama başladıktan sonra tarayıcınızda aşağıdaki adresi açarak blog sitesini görüntüleyebilirsiniz:

```
http://localhost:7087
```






