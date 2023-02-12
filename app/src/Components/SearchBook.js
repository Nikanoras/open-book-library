import React from "react";

const SearchBook = ({ onChangeSearch }) => {
  return (
    <div className="col-md-6">
      {" "}
      <input
        type="text"
        placeholder="ISBN number:"
        className="form-control"
        onChange={(e) => onChangeSearch(e.target.value)}
      />
    </div>
  );
};

export default SearchBook;
