import anthropic
from anthropic.types import Message, ContentBlock

client = anthropic.Anthropic()

# claude-haiku-4-5-20251001

def main() -> None:
    print("Making a call to the LLM...")

    message: Message = client.messages.create(
        model="claude-haiku-4-5-20251001",
        max_tokens=1000,
        messages=[
            {
                "role": "user",
                "content": "What should I search for to find the latest developments in renewable energy?",
            }
        ],
    )

    for block in message.content:
        if block.type == "text":
            print(block.text)
