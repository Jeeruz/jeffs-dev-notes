JeffsDevNotes.sln
│
├── JeffsDevNotes.Shared/                       <-- Shared Class Library (Optional but Recommended)
│   └── Models/
│       ├── Person.cs                            <-- Person entity / data transfer object (DTO)
│       └── CreatePersonRequest.cs               <-- Request payload model
│
├── JeffsDevNotes/                              <-- ASP.NET Core Server Host
│   ├── Controllers/
│   │   └── PeopleController.cs                  <-- Web API endpoints (GET, POST, PUT, DELETE)
│   ├── Components/
│   │   └── App.razor                            <-- HTML shell script references & WASM bootstrapper
│   └── Program.cs                               <-- Server-side middleware & DB pipeline
│
└── JeffsDevNotes.Client/                        <-- Client-Side WebAssembly Project
    ├── Layout/                                  <-- AppShell / Navigation Framework
    │   ├── MainLayout.razor                     <-- Root shell template (@Body host)
    │   ├── NavMenu.razor                        <-- Sidebar / Navbar navigation links
    │   └── UserProfileHeader.razor              <-- Header bar with user state or search
    │
    ├── Routes.razor                             <-- App Router (maps URLs to Razor pages)
    ├── _Imports.razor                           <-- Global namespaces (@using JeffsDevNotes.Client.Services)
    ├── Program.cs                               <-- Client DI (HttpClient, Services registration)
    │
    ├── Services/                                <-- API Communication Layer
    │   ├── IPersonService.cs                    <-- Interface contract
    │   └── PersonService.cs                     <-- HttpClient calls to /api/people
    │
    └── Pages/                                   <-- Feature Razor Pages
        └── People/                              <-- Person Module Folder
            ├── PersonList.razor                 <-- Route: @page "/people" (Table + Search + Delete)
            ├── PersonForm.razor                 <-- Route: @page "/people/create" & "/people/edit/{id:int}"
            ├── PersonDetail.razor               <-- Route: @page "/people/{id:int}"
            └── Components/                      <-- Module-Specific Sub-Components
                ├── PersonCard.razor             <-- Reusable UI card for a single person
                └── PersonSearchBox.razor        <-- Interactive filter input component