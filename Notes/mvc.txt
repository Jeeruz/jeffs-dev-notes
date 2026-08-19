JeffsDevNotes (Server Host Project)
│
├── Controllers/
│   └── EyeColorController.cs      <-- REST API (GET, POST, PUT, DELETE) connecting to DB
│
└── Program.cs                     <-- Registers DB context & API endpoints

────────────────────────────────────────────────────────────────────────────────

JeffsDevNotes.Client (Client WASM Project)
│
├── Models/
│   └── EyeColor.cs                <-- Data model shared across components
│
├── Services/
│   ├── IEyeColorService.cs        <-- Interface for CRUD operations
│   └── EyeColorService.cs         <-- Sends HttpClient calls (GET/POST/PUT/DELETE) to API
│
├── Pages/
│   ├── EyeColors/
│   │   ├── EyeColorList.razor     <-- Lists items + Search bar
│   │   ├── EyeColorForm.razor     <-- Create / Edit modal or view
│   │   └── EyeColorDetail.razor   <-- View single item details
│   └── Home.razor
│
├── Layout/
│   └── MainLayout.razor
│
└── Program.cs                     <-- Registers EyeColorService for Dependency Injection