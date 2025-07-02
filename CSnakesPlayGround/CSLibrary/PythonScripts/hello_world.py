def hello_world(name: str) -> str:
    return f"Hello, {name}!"

if __name__ == "__main__":
    name = "World"
    greeting = hello_world(name)
    print(greeting)