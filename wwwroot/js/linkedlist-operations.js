document.addEventListener("DOMContentLoaded", function () {
  const svg = document.getElementById("operationsSvg");
  const insertBtn = document.getElementById("insertBtn");
  const deleteBtn = document.getElementById("deleteBtn");
  const searchBtn = document.getElementById("searchBtn");
  const resetBtn = document.getElementById("resetOpsBtn");
  const completeBtn = document.getElementById("completeOperationsBtn");
  const feedback = document.getElementById("operationsFeedback");
  const cppCodeBlock = document.getElementById("cppCodeBlock");
  const operationHistory = document.getElementById("operationHistory");

  let list = [10, 20, 30];
  let highlightedValue = null;
  let activeOperation = "";
  let inserted = false;
  let deleted = false;
  let searched = false;
  let currentCodeOperation = null;
  let currentCodeActiveLine = null;

  function setCode(operation, activeLine) {
    currentCodeOperation = operation;
    currentCodeActiveLine = activeLine;
    const snippets = {
      insert: [
        "Node* current = head;",
        "while (current->data != 20) current = current->next;",
        "Node* newNode = new Node(25);",
        "newNode->next = current->next;",
        "current->next = newNode;",
      ],
      delete: [
        "Node* current = head;",
        "while (current->next->data != 25) current = current->next;",
        "Node* temp = current->next;",
        "current->next = current->next->next;",
        "delete temp;",
      ],
      search: [
        "Node* current = head;",
        "while (current != nullptr) {",
        "    if (current->data == 30) return true;",
        "    current = current->next;",
        "}",
      ],
    };

    const consoleBox = document.getElementById("operationsCodeConsole");
    const isExpanded = consoleBox && consoleBox.classList.contains("expanded");

    if (isExpanded) {
      cppCodeBlock.innerHTML = snippets[operation]
        .map((line, index) => {
          const active = index === activeLine ? " active-code-line" : "";
          return `<span class="linked-code-line${active}">${line}</span>`;
        })
        .join("");
    } else {
      const line = snippets[operation][activeLine];

      cppCodeBlock.innerHTML = `<span class="linked-code-line active-code-line">${line}</span>`;
    }
  }
  window.refreshOperationsCodeConsole = function () {
    if (currentCodeOperation !== null && currentCodeActiveLine !== null) {
      setCode(currentCodeOperation, currentCodeActiveLine);
    }
  };

  function clearSvg() {
    while (svg.lastChild && svg.lastChild.tagName !== "defs") {
      svg.removeChild(svg.lastChild);
    }
  }

  function getLayout() {
    const count = list.length;

    if (count >= 4) {
      return {
        startX: 55,
        spacing: 235,
        width: 135,
        height: 82,
        y: 120,
        pointerY: 105,
      };
    }

    return {
      startX: 90,
      spacing: 280,
      width: 165,
      height: 90,
      y: 120,
      pointerY: 105,
    };
  }

  function drawList(animationClass = "") {
    clearSvg();

    const layout = getLayout();

    list.forEach((value, index) => {
      const x = layout.startX + index * layout.spacing;
      const y =
        value === 25 && activeOperation === "insert" ? layout.y + 38 : layout.y;

      const isActive = value === highlightedValue;

      addNode(x, y, value, isActive, layout, animationClass);

      if (index < list.length - 1) {
        addLine(
          x + layout.width,
          y + layout.height / 2,
          x + layout.spacing,
          layout.y + layout.height / 2,
        );
      } else {
        addText(
          x + layout.width + 30,
          y + layout.height / 2 + 7,
          "NULL",
          20,
          "#6B7280",
        );
      }
    });

    if (highlightedValue !== null) {
      const index = list.indexOf(highlightedValue);

      if (index !== -1) {
        const pointerX =
          layout.startX + index * layout.spacing + layout.width / 2;

        addText(pointerX - 30, layout.pointerY - 75, "current", 18, "#facc15");
        addPointerLine(pointerX, layout.pointerY - 35, pointerX, layout.y - 10);
      }
    }
  }

  function addNode(x, y, value, isActive, layout, animationClass = "") {
    const group = document.createElementNS("http://www.w3.org/2000/svg", "g");

    group.setAttribute(
      "class",
      `${isActive ? "ops-node active" : "ops-node"} ${animationClass}`,
    );

    const rect = document.createElementNS("http://www.w3.org/2000/svg", "rect");
    rect.setAttribute("x", x);
    rect.setAttribute("y", y);
    rect.setAttribute("width", layout.width);
    rect.setAttribute("height", layout.height);
    rect.setAttribute("rx", 18);
    rect.setAttribute("fill", isActive ? "#fff1a8" : "url(#opsNodeGradient)");
    rect.setAttribute("stroke", isActive ? "#facc15" : "#202E53");
    rect.setAttribute("stroke-width", isActive ? 7 : 3);
    rect.setAttribute("filter", "url(#opsSoftShadow)");

    group.appendChild(rect);

    group.appendChild(
      createText(x + 22, y + 33, "Data: " + value, countTextSize(), "#202E53"),
    );

    group.appendChild(
      createText(x + 22, y + 62, "Next", countTextSize() - 2, "#6B7280"),
    );

    svg.appendChild(group);
  }

  function countTextSize() {
    return list.length >= 4 ? 17 : 20;
  }

  function createText(x, y, text, size, fill) {
    const t = document.createElementNS("http://www.w3.org/2000/svg", "text");
    t.setAttribute("x", x);
    t.setAttribute("y", y);
    t.setAttribute("font-size", size);
    t.setAttribute("fill", fill);
    t.setAttribute("font-weight", "700");
    t.textContent = text;
    return t;
  }

  function addText(x, y, text, size, fill) {
    svg.appendChild(createText(x, y, text, size, fill));
  }

  function addLine(x1, y1, x2, y2) {
    const line = document.createElementNS("http://www.w3.org/2000/svg", "line");
    line.setAttribute("x1", x1);
    line.setAttribute("y1", y1);
    line.setAttribute("x2", x2);
    line.setAttribute("y2", y2);
    line.setAttribute("stroke", "#06b6d4");
    line.setAttribute("stroke-width", 5);
    line.setAttribute("stroke-linecap", "round");
    line.setAttribute("marker-end", "url(#arrowhead)");
    svg.appendChild(line);
  }

  function addPointerLine(x1, y1, x2, y2) {
    const line = document.createElementNS("http://www.w3.org/2000/svg", "line");
    line.setAttribute("x1", x1);
    line.setAttribute("y1", y1);
    line.setAttribute("x2", x2);
    line.setAttribute("y2", y2);
    line.setAttribute("stroke", "#facc15");
    line.setAttribute("stroke-width", 6);
    line.setAttribute("stroke-linecap", "round");
    line.setAttribute("marker-end", "url(#arrowhead)");
    svg.appendChild(line);
  }

  function addHistory(message) {
    const li = document.createElement("li");
    li.textContent = message;
    operationHistory.prepend(li);
  }

  insertBtn.addEventListener("click", function () {
    activeOperation = "insert";
    setCode("insert", 3);

    feedback.innerHTML =
      "<strong>Insertion step 1:</strong> The new node 25 first points to the node after 20. This protects the connection to node 30.";

    highlightedValue = 20;
    drawList("shuffle-soft");

    setTimeout(function () {
      if (!list.includes(25)) {
        list.splice(2, 0, 25);
      }

      highlightedValue = 25;
      inserted = true;
      setCode("insert", 4);

      feedback.innerHTML =
        "<strong>Insertion step 2:</strong> Node 20 now points to node 25. The list becomes 10 → 20 → 25 → 30.";

      drawList("insert-pop");
      addHistory("Inserted node 25 after node 20");
      checkCompletion();
    }, 900);
  });

  deleteBtn.addEventListener("click", function () {
    activeOperation = "delete";
    setCode("delete", 3);

    if (!list.includes(25)) {
      feedback.innerHTML =
        "<strong>Deletion:</strong> Node 25 does not currently exist. Insert it first to observe deletion.";
      return;
    }

    highlightedValue = 25;
    feedback.innerHTML =
      "<strong>Deletion step 1:</strong> The algorithm finds node 25 and prepares to bypass it.";

    drawList("delete-warn");

    setTimeout(function () {
      list = list.filter((value) => value !== 25);
      highlightedValue = 20;
      deleted = true;
      setCode("delete", 4);

      feedback.innerHTML =
        "<strong>Deletion step 2:</strong> Node 20 now points directly to node 30. Node 25 is removed from the chain.";

      drawList("shuffle-soft");
      addHistory("Deleted node 25 by bypassing it");
      checkCompletion();
    }, 900);
  });

  searchBtn.addEventListener("click", function () {
    activeOperation = "search";
    setCode("search", 0);

    let searchPath = [10, 20, 30];
    let step = 0;

    function animateSearch() {
      if (step >= searchPath.length) return;

      highlightedValue = searchPath[step];
      setCode("search", step === searchPath.length - 1 ? 2 : 3);

      feedback.innerHTML = `<strong>Search:</strong> current is checking node ${searchPath[step]}.`;

      drawList("shuffle-soft");

      if (searchPath[step] === 30) {
        searched = true;
        feedback.innerHTML =
          "<strong>Search complete:</strong> Node 30 was found.";
        addHistory("Searched for node 30");
        checkCompletion();
        return;
      }

      step++;
      setTimeout(animateSearch, 750);
    }

    animateSearch();
  });

  resetBtn.addEventListener("click", function () {
    list = [10, 20, 30];
    highlightedValue = null;
    activeOperation = "";
    inserted = false;
    deleted = false;
    searched = false;
    completeBtn.disabled = true;

    cppCodeBlock.innerHTML =
      '<span class="linked-code-line">// Select an operation to begin.</span>';

    operationHistory.innerHTML = "<li>Initial list created: 10 → 20 → 30</li>";

    feedback.innerHTML = "The linked list currently contains 10 → 20 → 30.";

    drawList();
  });

  function checkCompletion() {
    if (inserted && deleted && searched) {
      completeBtn.disabled = false;
      feedback.innerHTML +=
        "<br><br><strong>All operations completed.</strong> You may now complete the challenge.";
    }
  }

  drawList();
});
