import os
from openai import OpenAI

def unicorn(api_key: str) -> str:
    client = OpenAI(api_key=api_key)

    completion = client.chat.completions.create(
        model="gpt-4o-mini",
        store=True,
        messages=[
            {"role": "user", "content": "Write a 10-sentence-long bedtime story about a unicorn."}
        ]
    )

    return completion.choices[0].message.content

if __name__ == "__main__":
    api_key = os.getenv("OPEN_AI_API_KEY")

    if not api_key:
        raise ValueError("Environment variable 'OPEN_AI_API_KEY' is not set or is empty.")

    response = unicorn(api_key)

    print(response)