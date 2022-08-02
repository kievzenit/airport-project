export default function createPagination(totalItemsCount, pageNumber, link) {
    if (totalItemsCount <= 6) { return; }

    let pagination = document.querySelector('.pagination');
    pagination?.remove();

    let container = document.querySelector('.container');

    let nav = document.createElement('nav');
    let ul = document.createElement('ul');

    ul.classList.add('pagination', 'justify-content-end');

    let pagesCount = Math.ceil(totalItemsCount / 6);

    let previous = document.createElement('li');

    if (pageNumber == 1) {
        let previousSpan = document.createElement('span');

        previous.classList.add('page-item', 'disabled');
        previousSpan.classList.add('page-link');

        previousSpan.innerText = 'Previous';

        previous.appendChild(previousSpan);
    } else {
        let previousLink = document.createElement('a');

        previous.classList.add('page-item');
        previousLink.classList.add('page-link');

        previousLink.innerText = 'Previous';
        previousLink.href = `/${link}/${pageNumber - 1}`;

        previous.appendChild(previousLink);
    }

    nav.appendChild(ul);
    ul.appendChild(previous);

    for (let i = 1; i <= pagesCount; i++) {

        let pageItem = document.createElement('li');

        pageItem.classList.add('page-item');

        if (pageNumber == i) {
            let pageSpan = document.createElement('span');
            pageSpan.innerText = i;

            pageSpan.classList.add('page-link');
            pageItem.classList.add('active');

            pageItem.setAttribute('aria-current', 'page');

            pageItem.appendChild(pageSpan);
        } else {
            let pageLink = document.createElement('a');
            pageLink.innerText = i;

            pageLink.classList.add('page-link');
            pageLink.href = `/${link}/${i}`;

            pageItem.appendChild(pageLink);
        }

        ul.appendChild(pageItem);
    }

    let next = document.createElement('li');

    if (pageNumber == pagesCount) {
        let nextSpan = document.createElement('span');

        next.classList.add('page-item', 'disabled');
        nextSpan.classList.add('page-link');

        nextSpan.innerText = 'Next';

        next.appendChild(nextSpan);
    } else {
        let nextLink = document.createElement('a');

        next.classList.add('page-item');
        nextLink.classList.add('page-link');

        nextLink.innerText = 'Next';
        nextLink.href = `/${link}/${parseInt(pageNumber) + 1}`;

        next.appendChild(nextLink);
    }

    ul.appendChild(next);
    container.appendChild(nav);
}