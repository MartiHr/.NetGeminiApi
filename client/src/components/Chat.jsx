import React, { useState } from 'react';

function App() {
    const [history, setHistory] = useState([]);
    const [inputText, setInputText] = useState('');
    const [response, setResponse] = useState('');

    // Function to handle submitting the conversation history to the API
    const handleSubmit = async () => {
        const newHistory = [...history, inputText];
        setHistory(newHistory);

        // Prepare the request payload
        const payload = newHistory;

        try {
            // Make a POST request to your .NET API
            const res = await fetch('https://localhost:7109/gemini/generate', { // API URL with HTTPS
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(payload),
            });

            if (res.ok) {
                const data = await res.json();
                setResponse(data.response);
            } else {
                throw new Error('Failed to fetch the response');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    };


    return (
        <div>
            <h1>Gemini AI Interaction</h1>

            <div>
                <h3>Conversation History</h3>
                <ul>
                    {history.map((msg, index) => (
                        <li key={index}>{msg}</li>
                    ))}
                </ul>
            </div>

            <div>
                <textarea
                    placeholder="Type your message..."
                    value={inputText}
                    onChange={(e) => setInputText(e.target.value)}
                />
            </div>

            <button onClick={handleSubmit}>Send</button>

            <div>
                <h3>Response:</h3>
                <p>{response}</p>
            </div>
        </div>
    );
}

export default App;
