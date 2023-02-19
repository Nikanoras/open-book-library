import React, { useState } from "react";
import SearchBook from "../Components/SearchBook";
import Loading from "../Components/Loading";
import axios from "axios";

const MainPage = ({ setBooks }) => {
  const api = "https://openlibrary.org";
  const [result, setResult] = useState([])
  const [isLoading, setIsLoading] = useState(false)
  const onSearch = async (isbnNumber) => {
    setIsLoading(true)
    setResult([])
    const bookData = await axios
      .get(`${api}/isbn/${isbnNumber}.json`)
      .then(response => response.data)
      .catch(error => {
        setIsLoading(false)
        console.log(error);
      });

    let book = {
      title: bookData.title,
      isbnNumber,
    }
    if (bookData) {
      if (bookData.authors) {
        const authors = await fetchAuthors(bookData);
        book = { ...book, authors };
      }

      const image = await fetchBookCover(isbnNumber)
      book = { ...book, image }
    }
    setResult([book])
    setIsLoading(false)
  };

  const fetchBookCover = async (isbnNumber) => {
    return await axios.get(`https://covers.openlibrary.org/b/isbn/${isbnNumber}-L.jpg`, { responseType: "arraybuffer" }).then(res => {
      const image = btoa(
        new Uint8Array(res.data).reduce(
          (data, byte) => data + String.fromCharCode(byte),
          ''
        )
      );
      return image;
    });
  }

  const fetchAuthors = async (bookData) => {
    const promises = [];
    for (let i = 0; i < bookData.authors.length; i++) {
      promises.push(new Promise((resolve) => {
        axios.get(`${api}${bookData.authors[i].key}.json`).then((response) => {
          resolve(response.data.name);
        });

      }));
    }
    return await Promise.all(promises).then((bookAuthors) => {
      return bookAuthors;
    });
  }

  return (
    <div className="row mt-5 d-flex justify-content-center">
      <SearchBook onChangeSearch={onSearch} />
      <div className="d-flex justify-content-center mt-5" >
        {!result.length && isLoading && <Loading />}
        {result.length !== 0 && !isLoading &&
          result.map(book =>
            <img key={book.title} className="img img-fluid" src={`data:image/jpg;base64,${book.image}`} alt={book.title} />
          )
        }
      </div>
    </div>
  );
};

export default MainPage;


