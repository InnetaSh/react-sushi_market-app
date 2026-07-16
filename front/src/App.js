
import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import MainLayout from './components/layout/MainLayout/MainLayout';
import Page1 from './pages/page_1';
import Page2 from './pages/page_2';
import Page3 from './pages/page_3';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          <Route index element={<Page1 />} />
          <Route path="menu" element={<Page2 />} />
          <Route path="menu/search/category/:category" element={<Page2 />} />
          <Route path="sale" element={<Page3 />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
