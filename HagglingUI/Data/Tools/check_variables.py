import json
import re
import os
from pathlib import Path

ALLOWED_VARIABLES = [
    "vendor_name",
    "vendor_age",
    "vendor_offer",
    "vendor_selling_point",
    "vendor_reputation",
    "customer_name", 
    "customer_age",
    "customer_offer",
    "item"
]

def find_unauthorized_variables(file_path):
    """
    Parse a JSON file and find unauthorized variable placeholders.
    Returns a list of tuples (line_number, variable_name, line_content)
    """
    unauthorized_vars = []
    
    with open(file_path, 'r', encoding='utf-8') as file:
        lines = file.readlines()
    
    for line_num, line in enumerate(lines, 1):
        placeholders = re.findall(r'\{([^{}]+)\}', line)
        
        for var in placeholders:
            if var.strip() not in ALLOWED_VARIABLES:
                unauthorized_vars.append((line_num, var.strip(), line.strip()))
    
    return unauthorized_vars

def main():
    base_dir = Path.cwd()
    customer_dialog_path = base_dir / "customer-dialog.json"
    vendor_dialog_path = base_dir / "vendor-dialog.json"
    
    files_to_check = [
        ("customer-dialog.json", customer_dialog_path),
        ("vendor-dialog.json", vendor_dialog_path)
    ]
    
    found_issues = False
    
    for file_name, file_path in files_to_check:
        if file_path.exists():
            print(f"Checking {file_name}...")
            unauthorized_vars = find_unauthorized_variables(file_path)
            
            if unauthorized_vars:
                found_issues = True
                print(f"Found unauthorized variables in {file_name}:")
                for line_num, var, line_content in unauthorized_vars:
                    print(f"  Line {line_num}: Unauthorized variable '{{{var}}}' in:")
                    print(f"    \"{line_content}\"")
                print()
            else:
                print(f"No unauthorized variables found in {file_name}")
        else:
            print(f"File not found: {file_name}")
    
    if not found_issues:
        print("All variables in both files are authorized.")

if __name__ == "__main__":
    main()