import seaborn as sns
import matplotlib.pyplot as plt
import io

try:
    from collections.abc import Buffer
except ImportError:
    from typing_extensions import Buffer

def graph() -> Buffer:
    plt.switch_backend('Agg')

    # Load the example dataset for tips
    tips = sns.load_dataset("tips")

    # Create a scatter plot with Seaborn
    plt.figure(figsize=(10, 6))
    sns.scatterplot(data=tips, x="total_bill", y="tip", hue="day", style="time", size="size", palette="viridis")

    # Add title and labels
    plt.title("Scatter Plot of Tips vs. Total Bill")
    plt.xlabel("Total Bill")
    plt.ylabel("Tip")

    # Save the plot to a bytes buffer
    buf = io.BytesIO()
    plt.savefig(buf, format='png')
    plt.close()
    buf.seek(0)

    # Convert _io.BytesIO to Buffer
    return memoryview(buf.getvalue())

if __name__ == "__main__":
    # Call the graph function
    buffer = graph()
    
    # Display the plot on the screen
    plt.switch_backend('TkAgg')  # Switch back to a GUI backend
    plt.imshow(plt.imread(io.BytesIO(buffer)))
    plt.show()