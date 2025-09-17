### **`tasks.json` Overview**
1. **Task Definitions**:
   - **Build**: Builds the entire solution (`SBA.sln`) using `dotnet build`. This task compiles the code without starting the services.
   - **Publish**: Publishes the entire solution (`SBA.sln`) using `dotnet publish`. This task prepares the solution for deployment, but doesn’t run the services.
   - **Run SBA.APIGateway Debug**: 
     - Runs the **API Gateway** service (`SBA.APIGateway`) in **watch mode** using `dotnet watch run`. This allows you to automatically rebuild and rerun the service when you make code changes.
   - **Run SBA.Auth Debug**: 
     - Runs the **Authentication** service (`SBA.Auth`) in **watch mode** using `dotnet watch run`.
   - **Run SBA.Hierarchy Debug**: 
     - Runs the **Hierarchy** service (`SBA.Hierarchy`) in **watch mode** using `dotnet watch run`.

2. **Compound Task**:
   - **Run All Services Debug**: This is a compound task that runs the following three individual tasks:
     - **Run SBA.APIGateway Debug**
     - **Run SBA.Auth Debug**
     - **Run SBA.Hierarchy Debug**
   - When you run this task, all three services will start in **watch mode** and be ready to debug.

---

### **`launch.json` Overview**
1. **Debugging Configurations**:
   - **SBA.APIGateway**:
     - This configuration will launch the **API Gateway** service for debugging.
     - It specifies a **preLaunchTask** to **build** the solution before launching the service.
     - Once the service is running, it will **open Swagger** (using `serverReadyAction`) at the specified URI (e.g., `/api/gateway/v1/swagger/index.html`).
   - **SBA.Auth**:
     - This configuration will launch the **Authentication** service for debugging.
     - It also has a **preLaunchTask** to **build** the solution before running the service.
     - It will open the Swagger UI for **Auth** once the service is ready.
   - **SBA.Hierarchy**:
     - This configuration will launch the **Hierarchy** service for debugging.
     - It includes a **preLaunchTask** to **build** before launching the service.
     - The Swagger UI for **Hierarchy** will open when the service is ready.

2. **Compound Debugging Configuration**:
   - **Run All Services**: This is a **compound configuration** that allows you to start and debug all three services (**APIGateway**, **Auth**, and **Hierarchy**) together in one go.
     - It references the debugging configurations for each service.

---

### **How These Work Together:**
1. **`tasks.json`**:
   - The **build**, **publish**, and **run tasks** are used for general development, where you either want to build, publish, or run the services in watch mode.
   - The **compound task** (`Run All Services Debug`) is used when you want to run all services at once (in watch mode).

2. **`launch.json`**:
   - This file is focused on **debugging**. It provides configurations that allow you to launch each service (or all services) for debugging.
   - The **preLaunchTask** ensures that the services are built before debugging.
   - The **serverReadyAction** automatically opens the Swagger UI for each service once it’s running.

---

### **In Practice:**
- **Building the services**: Use the `build` task in **`tasks.json`** (either manually or as a pre-launch task).
- **Running services in watch mode**: Use the individual **run tasks** (e.g., `Run SBA.APIGateway Debug`, etc.) or the **compound task** `Run All Services Debug` to start all services in watch mode.
- **Debugging services**: Use the configurations in **`launch.json`** to start the services and attach the debugger, with Swagger auto-opening once the service is running.