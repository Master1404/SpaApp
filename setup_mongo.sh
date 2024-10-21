#!/bin/bash

# Function to run the initialization script
run_init_script() {
    echo "Running initialization script..."
    mongosh -u admin -p admin123 --authenticationDatabase admin <<EOF
load('scripts/init-mongo.js')
EOF
    echo "Initialization script executed."
}

# Function to run the seed script
run_seed_script() {
    echo "Running seed script..."
    mongosh -u admin -p admin123 --authenticationDatabase admin <<EOF
load('scripts/seed-mock-data.js')
EOF
    echo "Seed script executed."
}

# Main script
run_init_script
run_seed_script
