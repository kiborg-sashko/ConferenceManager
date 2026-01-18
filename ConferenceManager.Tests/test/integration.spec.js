const axios = require('axios');
const { expect } = require('chai');

const API_URL = "http://localhost:5000/api"; // URL вашого Docker-контейнера

describe('Інтеграційні тести API (Mocha)', () => {

    // ПУНКТ 2: Перевірка версіонування (v1 vs v2)
    it('v1 повинен повертати список конференцій (сумісність)', async () => {
        const res = await axios.get(`${API_URL}/v1/Conferences`);
        expect(res.status).to.equal(200);
        expect(Array.isArray(res.data)).to.be.true;
    });

    it('v2 повинен працювати з новою структурою даних', async () => {
        const res = await axios.get(`${API_URL}/v2/Conferences`);
        expect(res.status).to.equal(200);
    });

    // ПУНКТ 3: Робота з БД (Створення запису)
it('Має успішно створювати нову конференцію (v1 POST)', async () => {
    const newConference = {
        name: "Тестова конференція Mocha",
        date: "2026-05-20T10:00:00Z",
        location: "Київ",
        description: "Опис для інтеграційного тесту"
    };

    const res = await axios.post('http://localhost:5000/api/v1/Conferences', newConference);
    
    // Тепер статус буде 201
    expect(res.status).to.equal(201);
    expect(res.data.name).to.equal(newConference.name);
});
});