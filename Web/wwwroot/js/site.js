async function loadPartial(container, url) {

    const response = await fetch(url);
    const html = await response.text();

    container.innerHTML = html;
}