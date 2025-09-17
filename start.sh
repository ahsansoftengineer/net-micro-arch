#!/bin/bash

## Without Debugging and Environment Settings

# 📁 Navigate to script directory
cd "$(dirname "$0")"

# 🧹 Function to clean up before and after the script execution
clean_up() {
    echo "🛑 Stopping services..."
    pkill -f "dotnet watch"
    echo "🧹 Cleaning up ports..."
    for port in {1100..1120}; do
        fuser -k ${port}/tcp 2>/dev/null
    done
    echo "🧼 Cleaning terminal..."
    sleep 1
    clear
}

# 🔄 Clean up before starting any execution
clean_up

# 🚨 Trap to clean up when the script terminates (cleanup after script execution)
trap '
    clean_up
    exit
' SIGINT SIGTERM EXIT

# 🚀 Start each microservice project in the background
echo "🚀 Starting microservices..."

# We Watch Only One Project
# https profile for Bash
dotnet watch --launch-profile https --no-restore --project ./SBA.APIGateway/SBA.APIGateway.csproj &
APIGateway_PID=$!
dotnet watch --launch-profile https --no-restore --project ./SBA.Auth/SBA.Auth.csproj &
Auth_PID=$!
dotnet watch --launch-profile https --no-restore --project ./SBA.Hierarchy/SBA.Hierarchy.csproj &
Hierarchy_PID=$!

# 🕒 Wait for all background jobs to start
wait

# 🌐 Open browser with the specific URL after starting the services
open_url="https://localhost:1100/"
echo "🌐 Opening browser with $open_url"
xdg-open "$open_url"  # Use xdg-open for Linux; on macOS, use open, on Windows use start

# 💤 Wait for all processes to finish
wait

# Cleanup will be triggered here automatically when the script ends.

## Tested Working on Watch Mode, No Debugging Support
# chmod +x .vscode/starts.sh 
# bash .vscode/starts.sh