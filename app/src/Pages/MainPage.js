import React from "react";
import SearchBook from "../Components/SearchBook";
import axios from "axios";

const MainPage = ({ setBooks }) => {
  const onChangeSearch = async (isbnNumber) => {
    await axios
      .get(`https://openlibrary.org/isbn/${isbnNumber}.json`)
      .then((response) => {
        const data = response.data;
        if (data) {
          setBooks({
            title: data.title,
            isbnNumber: data.isbn_13[0],
          });
        }
      })
      .catch((error) => console.log(error));
  };
  return (
    <div className="row mt-5 d-flex justify-content-center ">
      <SearchBook onChangeSearch={onChangeSearch} />
    </div>
  );
};

export default MainPage;
