import React, { useState } from "react";

const SearchBook = ({ onChangeSearch }) => {
  const [isbnNumber, setIsbnNumber] = useState("")
  return (
    <div className="col-md-3">
      <label htmlFor="isbnNumber" className="form-label">ISBN Number</label>
      <div className=" input-group">
        <span className="input-group-text">978</span>
        <input
          type="text"
          name="isbnNumber"
          className="form-control"
          maxLength={10}
          onChange={(e) => setIsbnNumber(`978${e.target.value}`)}
        />
        <button onClick={(e) => onChangeSearch(isbnNumber)} type="button" className="btn btn-dark">Search</button>
      </div>
    </div>
  );
};

export default SearchBook;
