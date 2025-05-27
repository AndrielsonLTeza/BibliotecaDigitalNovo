import { useEffect, useState } from "react";
import axios from "axios";

type Book = {
  id: number;
  title: string;
  author: string;
  genreId: number;
};

type Genre = {
  id: number;
  name: string;
};

export default function App() {
  const [books, setBooks] = useState<Book[]>([]);
  const [genres, setGenres] = useState<Genre[]>([]);

  useEffect(() => {
    axios.get("http://localhost:5000/api/Books").then((res) => setBooks(res.data));
    axios.get("http://localhost:5000/api/Genres").then((res) => setGenres(res.data));
  }, []);

  return (
    <div className="p-4 max-w-4xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Books</h1>
      <ul className="space-y-2">
        {books.map((book) => (
          <li
            key={book.id}
            className="p-4 border rounded-lg shadow hover:bg-gray-50"
          >
            <strong>{book.title}</strong> by {book.author}
          </li>
        ))}
      </ul>

      <h2 className="text-xl font-semibold mt-8 mb-4">Genres</h2>
      <ul className="flex flex-wrap gap-2">
        {genres.map((genre) => (
          <li
            key={genre.id}
            className="px-3 py-1 bg-blue-100 rounded-full text-blue-800"
          >
            {genre.name}
          </li>
        ))}
      </ul>
    </div>
  );
}
