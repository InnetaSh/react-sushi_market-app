
import './App.css';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

import MainLayout from './components/layout/MainLayout/MainLayout';
import MainPage from './pages/MainPage';
import MenuPage from './pages/MenuPage';
import Page3 from './pages/page_3';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          <Route index element={<MainPage />} />
          <Route path="menu" element={<MenuPage />} />
          <Route path="menu/search/category/:category" element={<MenuPage />} />
          <Route path="sale" element={<Page3 />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
