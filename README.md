# Order Processor
Event-driven integration with MCP for order processing.

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
The **Order Processor** project shows how MCP functionality can enable users to manage orders effectively without needing a complex interface. It also highlights event-driven architecture with a Consumer app (representing a 3PL or other app that the business would use to manage shipping orders) that consumes OrderCreated events and publishes Feedback events to a feedback EventHub that the Producer can listen to in order to update the status of processed orders.  

# Quick Demo


https://github.com/user-attachments/assets/f60a23ee-5faf-499e-ba01-07b4bf575ba8



# Tech Stack
- **Backend:** .NET 10
- **Database:** MS SQL Server

---

# Architecture Overview
<img width="2878" height="612" alt="image" src="https://github.com/user-attachments/assets/c8149b85-d074-48dc-ae47-b66cd2bf2b4a" />
[Created using [Mermaid Charts](https://www.mermaidchart.com/)]

This project is modeling **2 independent systems** - an application that users in a business use to create orders (the Producer), and an application that listens for order created events and picks them up. This would be a shipping/logistics app that a company or one of its 3PLs use for transportation of orders.

For simplicity, these were created in a single repo, but the 2 apps have no dependencies on each other to keep with how it'd work in the real world.

The Producer exposes MCP functions that allow users to get relevant data about orders and create new ones. When an order is created, it is persisted to the DB, and an OrderCreated event is placed on the `orders` EventHub.

The Consumer listens to the `orders` EventHub and (after waiting 10 seconds to simulate asynchronocity) publishes and orders-feedback event, telling the Producer that it's received the order for processing. The Producer then updates the status of that order as Processed.

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
- Copy-paste the `src/OrderProcessor/Migrations/CreateSchema.sql` into your `OrderProcessorProducer` DB.

### .NET Apps
- Open the `OrderProcessor` Solution in Visual Studio
- Reach out to me for the EventHub connection string to set in the `local.settings.json` files, or spin up your own `orders` and `orders-feedback` EventHubs and get a connection string for them.
- Set the `OrderProcessor.Producer` and `OrderProcessor.Consumer` as the Startup Projects, and Run.
- Once the apps are running, open the `.vscode\mcp.json` file in Visual Studio Code, and click Start.
- Open a GitHub Copilot chat in VS Code, and make sure the `order-processor-producer` MCP server and all of its associated tools are selected for use.

# Usage
Use GitHub Copilot to ask it whatever you'd like about orders and order creation! Some example prompts:
> Get me the status of all orders

> Create an order for me

> What products are available to include in an order?

> What shipping companies in Pennsylvania can I use to transport the order?

> Send the order to [receiving address] from [sending address]

> Get me the status of that order I just made.

> Has the status changed yet?

> Which orders have a Processed status?

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
