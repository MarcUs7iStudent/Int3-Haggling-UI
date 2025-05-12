import sys
import re

def format_text(input_text):
    """
    Format text by converting paragraphs into quoted, comma-separated strings.
    Each paragraph should be separated by one or more blank lines in the input.
    Ignores existing quotation marks in the input.
    """
    paragraphs = re.split(r'\n\s*\n', input_text.strip())
    
    formatted_lines = []
    for paragraph in paragraphs:
        clean_paragraph = re.sub(r'\s+', ' ', paragraph.strip())
        
        if not clean_paragraph:
            continue
        
        clean_paragraph = re.sub(r'^"(.*)"$', r'\1', clean_paragraph)
        formatted_lines.append(f'"{clean_paragraph}"')
    
    return ',\n'.join(formatted_lines)

def main():
    if len(sys.argv) > 1:
        input_file = sys.argv[1]
        try:
            with open(input_file, 'r', encoding='utf-8') as f:
                input_text = f.read()
        except Exception as e:
            print(f"Error reading file: {e}", file=sys.stderr)
            return 1
    else:
        print("Paste your text below (press Ctrl+D on Unix or Ctrl+Z followed by Enter on Windows when done):")
        input_text = sys.stdin.read()
    
    formatted_text = format_text(input_text)
    print(formatted_text)
    
    if len(sys.argv) > 2:
        output_file = sys.argv[2]
        try:
            with open(output_file, 'w', encoding='utf-8') as f:
                f.write(formatted_text)
            print(f"Formatted text saved to {output_file}")
        except Exception as e:
            print(f"Error writing to file: {e}", file=sys.stderr)
            return 1
    
    return 0

if __name__ == "__main__":
    sys.exit(main())