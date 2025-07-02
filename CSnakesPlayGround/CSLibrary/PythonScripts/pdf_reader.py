import sys
import os
import PyPDF2

def read(file_name: str) -> str:
    with open(file_name, 'rb') as file:
        pdf_reader = PyPDF2.PdfReader(file)
        text = ""
        for page_num in range(len(pdf_reader.pages)):
            page = pdf_reader.pages[page_num]
            text += page.extract_text()
    return text

if __name__ == "__main__":
    script_dir = os.path.dirname(os.path.abspath(__file__))  # Get the directory of the script
    default_file = os.path.join(script_dir, "CSnakes.pdf")  # Construct the path to CSnakes.pdf
    file_name = sys.argv[1] if len(sys.argv) > 1 else default_file
    text = read(file_name)
    print(text)    
