import {} from "react";

function App() {
  return (
    <div className="flex min-h-screen items-center justify-center">
      <button
        className="bg-moutarde-600 hover:bg-moutarde-500 rounded-lg px-8 py-3 font-semibold text-white transition-colors"
        onClick={() => alert("Hello World!")}
      >
        〽️ - Moutarde
      </button>
    </div>
  );
}

export default App;
