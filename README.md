# Order Processor
Event-driven integration with MCP for order processing

---

## Table of Contents
- [Introduction](#introduction)
- [Tech Stack](#tech-stack)
- [Architecture Overview](#architecture-overview)
- [Build and Run](#build-and-run)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

---

# Introduction
The **Order Processor** 

# Tech Stack
- **Backend:** .NET 10
- **Database:** MS SQL Server

---

# Architecture Overview

---

# Build and Run
## Prerequisites
- .NET 10 SDK  
- Visual Studio 2026 (Currently the only VS that can run .NET 10)
- SSMS v18 or later
- SQL Server Express

## Setup
- Set up a new SQL Server DB locally named `OrderProcessorProducer`. 
- Copy the connection string information into the `OrderProcessorProducer` connection string in the Infrastructure project's `appsettings.Development.json`.
  - Account used for connection string needs `db_datareader` and `db_datawriter` permissions on the DB.
- Open the `OrderProcessor` Solution in Visual Studio
- Copy-paste the `eng/sql/seed-data.sql` into your `OrderProcessorProducer` DB. 
- Copy-paste the `src/OrderProcessor/Migrations/Initial.sql` into your `OrderProcessorProducer` DB.

# Usage

# Contributing
Contributions are welcome!
Please open an issue or submit a pull request following the project’s coding style and guidelines.

# License
This project is licensed under a Non-Commercial License.
Use, modification, and distribution are allowed for non-commercial purposes only.
See the LICENSE file for details.

# Contact
Casey Lewis
casey.lewis08@gmail.com