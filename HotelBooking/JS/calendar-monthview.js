// Manual copying b/c JS Date is mutable, causing unintended side effects.
const getPrevMonth = date => {
  const copy = new Date(date.getTime());
  return new Date(
  copy.setMonth(date.getMonth() - 1, 1));

};

const getNextMonth = date => {
  const copy = new Date(date.getTime());
  return new Date(
  copy.setMonth(date.getMonth() + 1, 1));

};

// There are 42 cells (index 0..41) in the month view.
// Returns the index of the first day of the month.
function monthStartDay(date) {// ^^^ TODO: rename to reflect function
  let firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
  let wkday = firstDay.getDay(); // 0 = Sun, 6 = Sat
  return wkday;
}
// TODO: replace ^^^ 
function indexOfFirstday(date) {
  return monthStartDay(date);
}
function indexOfLastday(date) {
  return indexOfFirstday(date) + daysInMonth(date) - 1;
}

function getLocalTimestring(date) {
  const options = { hour: '2-digit', minute: '2-digit' };
  return date.toLocaleTimeString([], options);
}

function daysInMonth(date) {
  let nextMonthStart = new Date(date.getFullYear(), date.getMonth() + 1, 0); // trick: day = 1..31, 0 => day before first day of month.
  return nextMonthStart.getDate(); // 1..31
}

function cellNumsInMonth(date) {
  const startDay = monthStartDay(date);
  const numOfDays = daysInMonth(date);

  let cellNums = [];
  for (var i = startDay; i < startDay + numOfDays; i++) {
    cellNums.push(i);
  }

  return cellNums;
}

// Assign a class to a cell depending on whether it is in or outside the month of the given date.
// cells: HTMLCollection of li elements in the monthview (not the month)
// date: Date
function classifyMonthCells(date, cells) {
  const validCellNums = cellNumsInMonth(date);

  const cellsInMonth = Array.from(cells).filter((elem, i) => {
    return validCellNums.includes(i);
  });

  const cellsOutsideMonth = Array.from(cells).filter((elem, i) => {
    return !validCellNums.includes(i);
  });

  for (const cell of cellsInMonth) {
    cell.classList.add('inside-month');
    cell.classList.remove('outside-month');
  }

  for (const cell of cellsOutsideMonth) {
    cell.classList.add('outside-month');
    cell.classList.remove('inside-month');
  }

  return;
}

function numberMonthCells(date, cells) {
  const validCellNums = cellNumsInMonth(date);

  const cellsInMonth = Array.from(cells).filter((elem, i) => {
    return validCellNums.includes(i);
  });

  for (const [index, cell] of cellsInMonth.entries()) {
    const txt = document.createTextNode(`${index + 1}`);
    const span = document.createElement('span');
    span.classList.add('day-number');
    span.appendChild(txt);
    cell.appendChild(span);
  }

  return;
}

function updateCalendarTitle(date, titleElement) {
  const months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

  titleElement.textContent = `${months[date.getMonth()]} ${date.getFullYear()}`;

  return;
}

function showPostsInMonth(dateInMonth, posts, cells) {

  const monthStart = new Date(dateInMonth.getFullYear(), dateInMonth.getMonth(), 1);
  const monthEnd = new Date(dateInMonth.getFullYear(), dateInMonth.getMonth() + 1, 1);

  for (const post of posts) {

    // ignore if post is not in this month. @rethink
    if (post.publishAt.getTime() < monthStart.getTime() ||
    post.publishAt.getTime() >= monthEnd.getTime()) {
      continue;
    }

    // get cell corresponding to post date
    const targetCellNum = monthStartDay(dateInMonth) +
    post.publishAt.getDate() - 1;
    const cellForPost = cells[targetCellNum];

    // construct and add post node to cell
    const postBlock = document.createElement('div');

    // add font-awesome icon corresponding to post's channel
    if (post.channelID && icon4channel(post.channelID)) {
      const channelIcon = document.createElement('i');
      channelIcon.classList.add('fab');
      channelIcon.classList.add(icon4channel(post.channelID));
      postBlock.appendChild(channelIcon);

      postBlock.classList.add(`channel-${post.channelID}`);
    }

    // add post title
    const postTitle = document.createElement('span');
    const postTime = getLocalTimestring(post.publishAt);
    postTitle.textContent = `${postTime}`;
    postBlock.appendChild(postTitle);

    postBlock.classList.add('post-item');
    postBlock.setAttribute('id', `post-${post.id}`); // eg id='post-1'

    if (config.isDraggable) {
      postBlock.setAttribute('draggable', 'true');

      postBlock.addEventListener('drag', config.drag, false);
      postBlock.addEventListener('dragstart', config.dragStart, false);
      postBlock.addEventListener('dragend', config.dragEnd, false);
      postBlock.addEventListener('dragexit', config.dragExit, false);
    }

    if (config.postClickHandler) {
      postBlock.addEventListener('click', config.postClickHandler, false);
    }

    if (config.postHoverHandler) {
      postBlock.addEventListener('mouseover', config.postHoverHandler, false);
    }

    cellForPost.appendChild(postBlock);
  }

  if (config.isDraggable) {//! config is global var
    makeCellsDroppable();
  }

  conditionallyShowMoreButton(); // @abandoned
}

// Show '+x more' button when the number of posts in a cell
// overflow the cell height.
// @abandoned because an elegant implementation requires the ability
// to listen for size changes on the cell element. To date, browsers do not support a 'size-change/resize' for elements besides the window element. Other workarounds require regular polling using setTimeout which is quite ugly.
function conditionallyShowMoreButton() {//@debug only
  const listOfcellCanvas = document.querySelectorAll('.days li .content');
  for (const cellCanvas of listOfcellCanvas) {
    cellCanvas.addEventListener('resize', ev => {
      // topInset is the height of by the day-number, don't consider this when computing height
      const insetElem = ev.currentTarget.querySelector('.day-number');
      const topInset = insetElem ? insetElem.offsetHeight : 0;
      const contentHeight = ev.currentTarget.scrollHeight - topInset;
      const visibleHeight = ev.currentTarget.clientHeight - topInset;

      // if posts overflow cell
      if (contentHeight > visibleHeight) {
        // determine the number of posts which fit
        const children = ev.currentTarget.querySelectorAll('.post-item'); //! magic string
        const childHeight = contentHeight / children.length;
        const numThatFit = Math.floor(visibleHeight / childHeight);
        // hide overflowing posts
        children.forEach((node, i) => {
          // numThatFit - 1 => making room for the 'more-posts' btn
          node.style.visibility = i < numThatFit - 1 ? 'visible' : 'hidden';
          node.style.position = i < numThatFit - 1 ? 'relative' : 'absolute';
        });
        // show a more btn.
        const moreBtn = document.createElement('button');
        moreBtn.classList.add('more-btn'); // TODO: put in API
        const moreText = document.createTextNode(`+ ${children.length - numThatFit + 1} More`);
        moreBtn.appendChild(moreText);
        ev.currentTarget.appendChild(moreBtn);

        console.log(`num-of-children: ${children.length}, child-height: ${childHeight}, num-that-fit: ${numThatFit}`);
      }
      console.log(`content-height: ${contentHeight} - visible-height: ${visibleHeight}`);
    }, false);
  }
}

function highlightToday(calDate, monthCells) {
  console.log(monthCells);

  const now = new Date();

  // if today is not in current visible month, no need to hightlight.
  if (calDate.getFullYear() != now.getFullYear() ||
  calDate.getMonth() != now.getMonth()) {
    return;
  }
  // today can be any one of index 0..41 cells
  const indexOfToday = monthStartDay(now) + now.getDate() - 1; // b/c zero-indexed
  monthCells[indexOfToday].classList.add('today');
}

function makeCellsDroppable() {//? what cells
  if (!config.isDraggable) {//! config is global
    return;
  }

  const listOfcellCanvas = document.querySelectorAll('.days li .content');
  for (const cellCanvas of listOfcellCanvas) {
    cellCanvas.addEventListener('dragover', config.dragOver, false);
    cellCanvas.addEventListener('dragenter', config.dragEnter, false);
    cellCanvas.addEventListener('dragleave', config.dragLeave, false);
    cellCanvas.addEventListener('drop', config.drop, false);
  }
}

function addNavButtonActions() {
  const prevBtn = document.querySelector('.calendar header button:nth-of-type(1)');
  prevBtn.onclick = event => {
    const currDate = getCalendarDate();
    const prevMonth = getPrevMonth(currDate);
    renderMonthview(prevMonth, getPosts()); //! always fetching posts?
  };

  const todayBtn = document.querySelector('.calendar header button:nth-of-type(2)');
  todayBtn.onclick = event => {
    const now = new Date();
    renderMonthview(now, getPosts()); //! always fetching posts?
  };

  const nextBtn = document.querySelector('.calendar header button:nth-of-type(3)');
  nextBtn.onclick = event => {
    const currDate = getCalendarDate();
    const nextMonth = getNextMonth(currDate);
    renderMonthview(nextMonth, getPosts()); //! always fetching posts?
  };
}

// f: int -> nullable<string>
function icon4channel(channelID) {
  const m = new Map();
  m.set(1, 'fa-facebook-f');
  m.set(2, 'fa-instagram');
  m.set(3, 'fa-linkedin-in');
  m.set(4, 'fa-google');
  m.set(5, 'fa-wordpress-simple');
  m.set(6, 'fa-pinterest-p');
  m.set(7, 'fa-twitter');


  return m.get(channelID) || null;
}

// the date this calendar is currently showing, stored in a date input.
// f: void -> Date
function getCalendarDate() {
  const stateElem = document.getElementById(`calendar-date`);
  const currDate = stateElem.getAttribute('data-date');
  return new Date(currDate); // what if currDate is null?
}

// stores date in an input[type='date']
function setCalendarDate(date) {
  const stateElem = document.getElementById(`calendar-date`);

  /*
  // zero-pad values to fit format when necessary.
  const yyyy = date.getFullYear()
  const MM = (date.getMonth() + 1) < 10 ? `0${date.getMonth() + 1}` : `${date.getMonth() + 1}`
  const dd = date.getDate() < 10 ? `0${date.getDate()}`: `${date.getDate()}`
  stateElem.value = `${yyyy}-${MM}-${dd}`
  console.log(stateElem.value)
  */
  stateElem.setAttribute('data-date', date.toISOString());
}

function clearMonthCells(monthCells) {
  for (const cell of monthCells) {
    cell.innerHTML = '';
    // remove all classes, except the very type of this cell i.e 'content'
    cell.className = 'content';
  }
}

function getPosts() {
  const posts = [
  {
    id: 1, // must be unique - UUID recommended. if not unique, dragging misbehaves.
          title: 'Facebook Post 1',
    RoomType: 'Standard-10',
          publishAt: new Date('2024-03-05T12:00:00-03:30'),
        
          channelID: 1
      },

  {
    id: 2,
      title: 'Instagram Post 1',
      RoomType: 'Standard-11',
    publishAt: new Date('2024-03-05T14:00:00-03:30'),
    channelID: 2 },

  {
    id: 3,
    title: 'LinkedIn Post 1',
    publishAt: new Date('2018-02-09T16:00:00-03:30'),
    channelID: 3 },

  {
    id: 4,
    title: 'Google Plus Post 1',
    publishAt: new Date('2018-02-12T18:00:00-03:30'),
    channelID: 4 },

  {
    id: 5,
    title: 'Wordpress Post 1',
    publishAt: new Date('2018-02-14T20:00:00-03:30'),
    channelID: 5 },

  {
    id: 6,
    title: 'Pinterest Post 1',
    publishAt: new Date('2018-02-16T20:00:00-03:30'),
    channelID: 6 },

  {
    id: 7,
    title: 'Twitter Post 1',
    publishAt: new Date('2018-02-21T20:00:00-03:30'),
    channelID: 7 }];


  return posts;
}

function renderMonthview(date, posts) {
  setCalendarDate(date);
  const calendarTitleElem = document.getElementById(`calendar-title`);
  updateCalendarTitle(date, calendarTitleElem);

  // why am i using .content instead of just li?
  const monthCells = document.querySelectorAll('.days li .content');
  // wip: refactor to use li
  const _monthCells = document.querySelectorAll('.days li');

  clearMonthCells(monthCells);
  // classifyMonthCells(date, monthCells)
  classifyMonthCells(date, _monthCells);
  numberMonthCells(date, monthCells);

  if (posts) {
    showPostsInMonth(date, posts, monthCells);
  }

  if (config.highlightToday) {//! global var 'config'
    highlightToday(date, _monthCells);
  }

  hideCellsInUnusedRows(date, _monthCells);
}

function hideCellsInUnusedRows(date, monthCells) {

  // get position of last day of month
  const lastDayIndex = indexOfLastday(date);

  // if last day of month is not in last row
  // monthview is 7 x 6 grid i.e 42 cells. => index 35..41 is last row.
  if (lastDayIndex < 35) {//! magic #
    // hide all cells in last row
    for (let i = 35; i < monthCells.length; i++) {
      monthCells[i].classList.add('in-unused-row');
    }
  } else {
    // show all cells in last row
    for (let i = 35; i < monthCells.length; i++) {
      monthCells[i].classList.remove('in-unused-row');
    }
  }
}

const config = {
  isDraggable: true,
  /* on draggable target */
  drag: ev => {// called repeatedly
    //...?
    // console.log(`${Date.now()} - DRAG`)
  },
  dragStart: ev => {
    // console.log(`${Date.now()} - DRAG_START`)
    // probably change the cursor
    ev.dataTransfer.setData("text/plain", ev.target.id); //id?
    ev.dataTransfer.effectAllowed = "move";
  },
  dragEnd: ev => {
    // console.log(`${Date.now()} - DRAG_END`)
    // check success/failure status using dropEffect property
    // remove dragged element from old location if op=move
  },
  dragExit: ev => {
    // console.log(`${Date.now()} - DRAG_EXIT`)
    // check success/failure status using dropEffect property
    // remove dragged element from old location if op=move
  },
  /* on drop target */
  dragOver: ev => {// called continously
    ev.currentTarget.classList.add('cell-dragover'); // TODO: put in API
    // console.log(`${Date.now()} - DRAG_OVER`)
    ev.preventDefault();
    ev.dataTransfer.dropEffect = "move";
  },
  dragEnter: ev => {
    // console.log(`${Date.now()} - DRAG_ENTER`)
  },
  dragLeave: ev => {
    ev.currentTarget.classList.remove('cell-dragover'); // TODO: put in API
    // console.log(`${Date.now()} - DRAG_LEAVE`)
  },
  drop: ev => {
    // console.log(`${Date.now()} - DROP`)
    ev.currentTarget.classList.remove('cell-dragover');
    const postId = ev.dataTransfer.getData("text/plain");
    // Beware: ev.currentTarget is the registered droppable node
    // while ev.target could be any element in the droppable node subtree
    ev.currentTarget.appendChild(document.getElementById(postId));
    // TODO: business logic like change post's start/end day, and notify server.
    ev.preventDefault();
  },
  highlightToday: true,
  postClickHandler: ev => {
    console.log(`Clicked on post: ${ev.currentTarget.id}`);
  },
  postHoverHandler: ev => {
    console.log(`Hover on post: ${ev.currentTarget.RoomType}`);
  },
  useMilitaryTime: false };


function doStuff() {
    //const date = new Date('2018-02-23T12:00:00-03:30');
    const date = new Date($.now());
  renderMonthview(date, getPosts());
  addNavButtonActions(); //?
}

window.onload = doStuff;