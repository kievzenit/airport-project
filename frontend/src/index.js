import React from 'react';
import ReactDOM from 'react-dom/client';
import './styles/index.css';

import Airports from './components/Airports';
import Flights from './components/Flights';
import Passengers from './components/Passengers';

import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<Airports />} />
      <Route path='/passengers' element={<Passengers />} />
      <Route path='/flights' element={<Flights />} />
    </Routes>
  </BrowserRouter>
);