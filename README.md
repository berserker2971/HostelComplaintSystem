# 📌 Hostel Maintenance & Complaint Management System

A QR-based web application for reporting, tracking, and managing hostel maintenance issues. This system allows **students** to submit complaints by scanning QR codes, while **admins** monitor and resolve issues through a dashboard.

---

## 🌐 Live Website

🔗 **Hosted at:**  
➡️ [https://your-render-link.onrender.com](https://hostelcomplaintsystem-e1mi.onrender.com/)

*(Replace with your actual Render deployment URL)*

---

## 📸 How It Works

### ✔️ Student Workflow
- Scan QR codes placed at hostel locations.
- Complaint form auto-fills the location.
- Choose issue type (Plumbing, Electrical, Cleaning, etc.).
- Add description.
- Optionally upload a photo.
- Submit — no login required.

### ✔️ Admin Workflow
- Login using admin credentials.
- View all complaints in a sortable/filterable dashboard.
- Edit complaint status (New, In Progress, Resolved).
- View complaint images.
- Delete complaints.
- Generate and download QR codes.

---

## 🖥️ Features
- Location-based QR codes.
- Complaint submission with optional images.
- Admin authentication.
- Status-tracking dashboard.
- QR code generator with PNG download.
- Clean UI with Bootstrap 5.
- SQLite database.
- ASP.NET Core Identity.
- Full Docker deployment.

---

## 🚀 Deployment

This project is containerized with Docker and deployed to **Render Web Services**.

- Runtime: .NET 8
- Database: SQLite
- Auto admin seeding
- Dockerfile handles build + publish steps

---

## 💾 Default Admin Credentials

```
Email: admin@hostel.com
Password: Admin@123
```

Change these in **Program.cs** if required.

---

## 🧭 Usage Guide

### Students
1. Scan QR code installed at various hostel locations.
2. The complaint page opens automatically.
3. Select issue type + description.
4. Upload image (optional).
5. Submit.

### Admins
1. Login → redirected to dashboard.
2. View complaints.
3. Filter by status.
4. Update status or delete.
5. Generate new location QR codes.
6. Download QR code PNGs.

---

## 📂 Project Structure

```
HostelComplaintSystem/
├── Controllers/
│   ├── HomeController.cs
│   ├── ComplaintsController.cs
│   └── QrCodeController.cs
│
├── Models/
│   └── Complaint.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Areas/Identity/ (Login & Register pages)
│
├── Views/
│   ├── Home/
│   ├── Complaints/
│   ├── QrCode/
│   └── Shared/
│
├── wwwroot/uploads/ (student images)
│
├── Program.cs
└── Dockerfile
```

---

## 🛠️ Tech Stack
- ASP.NET Core MVC
- Razor Views
- Entity Framework Core
- ASP.NET Core Identity
- SQLite
- Bootstrap 5
- QRCoder library
- Docker
- Render Hosting

---

## 📌 Future Improvements
- Complaint tracking ID for students
- Email/SMS notifications
- Multi-role admin (Electrician, Plumber, Warden, etc.)
- Analytics dashboard

---

## 📄 License
This project is built for academic and demonstration purposes. You may reuse or modify it as needed.
