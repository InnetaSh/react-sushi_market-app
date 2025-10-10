
import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';


import Page1 from './html/page_1';
import Page2 from './html/page_2';
import Page3 from './html/page_3';

function App() {
  return (
    <Router>
    <Routes>
      <Route path="/" element={<Page1 />} />
      <Route path="/menu" element={<Page2 />} />
      <Route path="/menu/search/category/:category" element={<Page2 />} />
      <Route path="/sale" element={<Page3 />} />
    </Routes>
  </Router>
  );
}

export default App;
