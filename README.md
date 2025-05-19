# 🏋️ Spor Salonu Takip Sistemi

Bu proje, spor salonu üyelerini ve üyeliklerini takip etmek için geliştirilen basit ve modüler bir **.NET Console Uygulamasıdır**.  
Veritabanı işlemleri için **Entity Framework Core + MySQL** kullanılmıştır.

---

## 🚀 Özellikler

- 👥 Müşteri (üye) kaydı oluşturma
- 🔄 TC Kimlik numarasıyla üye bilgisi güncelleme
- 🆕 Gün bazlı salon üyeliği tanımlama
- ❌ Üyelik silme (seçilebilir ID ile)
- 📄 Tüm üyelikleri listeleme veya TC ile görüntüleme
- 🔍 TC Kimlik No ile üye ve üyelik detaylarını birlikte görme
- 🎯 Türkçe karakter desteği ve görselli menü
- ❗ Boş alan kontrolü ve TC doğrulaması
- 🛡️ Veritabanı seviyesinde benzersizlik (TC Kimlik için)

---

## 🛠️ Kullanılan Teknolojiler

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- Entity Framework Core
- Pomelo.EntityFrameworkCore.MySql (MySQL EF provider)
- MySQL 8.x
- C# 11

---

## 🧱 Kurulum

### 1. Depoyu klonla

```bash
git clone https://github.com/kullaniciAdi/spor-salonu-takip.git
cd spor-salonu-takip
```
### 2. Bağımlılıkları yükle

```bash
dotnet restore
```
### 3. MySQL bağlantısı ayarla
📂 Data/AppDbContext.cs dosyasındaki bağlantı cümlesini güncelle:
```bash
var conn = "server=localhost;database=spor_salon;user=root;password=123456;";
```
## 4. Çalışmaya Hazır Tablolar otomatik olarak Bağladığınız Veritabanına Göre oluşturulacaktır
