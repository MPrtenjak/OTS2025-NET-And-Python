import sys

from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.chrome.service import Service
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.by import By
from webdriver_manager.chrome import ChromeDriverManager

from bs4 import BeautifulSoup
import pandas as pd

import json

def fetch_by_data_id(data_id: str, web_driver_location: str) -> pd.DataFrame:
    url = "https://ljse.si/si/tecajnica/36?model=ALL&type=ALL"

    chrome_options = Options()
    chrome_options.add_argument("--headless")
    chrome_options.add_argument("--no-sandbox")
    chrome_options.add_argument("--disable-dev-shm-usage")

    if web_driver_location:
        service = Service(executable_path=web_driver_location)
    else:
        service = Service(ChromeDriverManager().install())

    driver = webdriver.Chrome(service=service, options=chrome_options)

    url = "https://ljse.si/si/tecajnica/36?model=ALL&type=ALL"
    driver.get(url)

    try:
        WebDriverWait(driver, 15).until(
            EC.presence_of_element_located((By.CSS_SELECTOR, f"table[data-id='{data_id}']"))
        )

        soup = BeautifulSoup(driver.page_source, "html.parser")
        table = soup.find("table", attrs={"data-id": data_id})

        if not table:
            raise ValueError(f"No table found with data-id='{data_id}'")

        headers = [th.get_text(strip=True) for th in table.find("thead").find_all("th")]
        rows = []
        for row in table.find_all("tr"):
            cells = [td.get_text(strip=True) for td in row.find_all("td")]
            if cells:
                rows.append(cells)

        return pd.DataFrame(rows, columns=headers if headers else None)

    finally:
        driver.quit()
    
def fetch_by_data_id_list(data_id: str, web_driver_location: str) -> list[dict[str, any]]:
    return fetch_by_data_id(data_id, web_driver_location).to_dict(orient="records")

def fetch_by_data_id_json(data_id: str, web_driver_location: str) -> str:
    return json.dumps(fetch_by_data_id(data_id, web_driver_location).to_dict(orient="records"))

if __name__ == "__main__":
    try:
        web_driver_location = sys.argv[1] if len(sys.argv) > 1 else None

        # table = fetch_by_data_id("A", web_driver_location)
        # table = fetch_by_data_id_list("A", web_driver_location)
        print("web_driver_location = ")
        print(web_driver_location)
        table = fetch_by_data_id_json("A", web_driver_location)
        print("------------------------------------------- B")
        print(table)
    except Exception as e:
        print(f"An error occurred: {e}")