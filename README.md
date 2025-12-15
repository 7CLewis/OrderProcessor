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
- Visual Studio 2026 (Currently the only VS version that will run .NET 10)
- Visual Studio Code with GitHub Copilot
- SSMS v18 or later
- SQL Server Express

## Setup
### DB
- Set up a new SQL Server DB locally named `OrderProcessorProducer`. 
- Copy the connection string information into the `OrderProcessorProducer` connection string in the Infrastructure project's `appsettings.Development.json`.
  - Account used for connection string needs `db_datareader` and `db_datawriter` permissions on the DB.
- Open the `OrderProcessor` Solution in Visual Studio
- Copy-paste the `eng/sql/seed-data.sql` into your `OrderProcessorProducer` DB. 
- Copy-paste the `src/OrderProcessor/Migrations/Initial.sql` into your `OrderProcessorProducer` DB.

### Producer MCP Server
- Open the `OrderProcessor` Solution in Visual Studio
- Reach out to me for the EventHub connection string to set in `local.settings.json`
- Set the `OrderProcessor.Producer` as the Startup Project and Run.
- Once the app is running, open the `.vscode\mcp.json` file in Visual Studio Code, and click Start.
- Open a GitHub Copilot chat in VS Code, and make sure the `order-processor-producer` MCP server and all of its associated tools are selected for use.

# Usage
Use GitHub Copilot to ask it whatever you'd like about orders and order creation! Some example prompts:
> Create an order for me

> What products are available to include in an order?

> What shipping companies in Pennsylvania can I use to transport the order?

> Send the order to [receiving address] from [sending address]

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