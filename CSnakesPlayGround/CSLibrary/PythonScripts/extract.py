import PyPDF2
import sys

def extract_pdf_texts(file_paths):
    contents = []
    for path in file_paths:
        try:
            with open(path, 'rb') as f:
                reader = PyPDF2.PdfReader(f)
                text = ""
                for page in reader.pages:
                    text += page.extract_text() or ""
                contents.append(text)
        except Exception as e:
            contents.append(f"[ERROR reading {path}: {e}]")
    return contents

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python extract.py <pdf1> <pdf2> ...")
    else:
        pdf_paths = sys.argv[1:]
        results = extract_pdf_texts(pdf_paths)
        for i, content in enumerate(results, 1):
            print(f"\n--- PDF {i} ---\n{content[:500]}...")  # Show only the first 500 characters
