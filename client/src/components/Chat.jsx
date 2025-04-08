import React, { useState } from 'react';

const Chat = () => {
  const [history, setHistory] = useState([]);
  const [input, setInput] = useState('');
  const [response, setResponse] = useState('');

  const handleInputChange = (e) => {
    setInput(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newHistory = [...history, { UserMessage: input }];
    setHistory(newHistory);

    try {
      // Send user input to the backend API
      const res = await fetch('https://localhost:7109/gemini/generate', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newHistory),
      });
      const data = await res.json();
      setResponse(data); // Store the response in state
    } catch (error) {
      console.error('Error:', error);
    }

    setInput(''); // Reset input field
  };

  return (
    <div>
      <h1>Chat with AI</h1>
      <div>
        {history.map((message, index) => (
          <div key={index}>
            <strong>User:</strong> {message.UserMessage}
            <br />
            <strong>AI:</strong> {message.AiResponse}
          </div>
        ))}
      </div>
      <form onSubmit={handleSubmit}>
        <input
          type="text"
          value={input}
          onChange={handleInputChange}
          placeholder="Type your message"
        />
        <button type="submit">Send</button>
      </form>
      {response && <div><strong>AI Response:</strong> {response}</div>}
    </div>
  );
};

export default Chat;
