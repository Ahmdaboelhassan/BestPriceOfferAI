# 🧠 Intelligent Excel Data Classifier and Price Optimizer (.NET Core API)

## 📘 Overview
This project is a **.NET Core Web API** designed to process and analyze **multiple unstructured Excel files**.  
It uses a **machine learning classification model** to identify and categorize Excel headers (such as `Name`, `Price`, `Discount1`, `Discount2`, etc.), then applies **fuzzy matching** techniques to find and group similar item names across files.  
Finally, it calculates and returns the **best final price** after applying the detected discount rules.

## 🚀 Features
- 📂 Handles multiple Excel files with unstructured data formats  
- 🤖 Classifies headers automatically using a trained ML model  
- 🔍 Performs fuzzy matching to detect similar item names  
- 💰 Applies multi-level discount logic to calculate optimal prices  
- ⚙️ Exposes results via RESTful API endpoints  
- 🧾 Supports structured output for downstream integrations  

## 🧱 Architecture
The solution is built using **Clean Architecture principles**, separating concerns into layers:
- **API Layer:** Handles HTTP requests and responses  
- **Application Layer:** Business logic, ML classification, and fuzzy matching  
- **Infrastructure Layer:** Excel parsing, EF Core data access, and configuration  
- **Domain Layer:** Core entities and value objects  

## 🛠️ Technologies Used
- **.NET Core / ASP.NET Core Web API**  
- **Entity Framework Core**  
- **ML.NET** – for header classification  
- **FuzzySharp** – for fuzzy string matching  
- **ClosedXML / EPPlus** – for Excel file handling  
- **AutoMapper** – for object mapping  

## ⚡ How It Works
1. Upload or provide paths to unstructured Excel files.  
2. The API parses and classifies headers into known categories (e.g., `Name`, `Price`, `Discounts`).  
3. Fuzzy matching groups similar item names across files.  
4. Business logic applies discount rules and computes the best final price.  
5. Results are returned as structured JSON data.

 


