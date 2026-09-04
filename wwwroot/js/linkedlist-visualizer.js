document.addEventListener("DOMContentLoaded", function () {
  console.log("linkedlist visualizer loaded");
  const svg = document.getElementById("linkedListSvg");
  const addNodeBtn = document.getElementById("addNodeBtn");
  const traverseBtn = document.getElementById("traverseBtn");
  const resetListBtn = document.getElementById("resetListBtn");
  const completeBtn = document.getElementById("completeChallengeBtn");
  const feedback = document.getElementById("challengeFeedback");
  const codeBlock = document.getElementById("linkedListCodeBlock");

  const nodeValues = [10, 20, 30];
  let nodes = [];
  let traverseIndex = -1;

  let currentCodeActiveLine = 0;

  function setCode(activeLine) {
    currentCodeActiveLine = activeLine;

    const lines = [
      "Node* current = head;",
      "",
      "while (current != nullptr) {",
      "    cout << current->data;",
      "    current = current->next;",
      "}",
    ];

    const consoleBox = document.getElementById("traversalCodeConsole");
    const isExpanded = consoleBox && consoleBox.classList.contains("expanded");

    if (isExpanded) {
      codeBlock.innerHTML = lines
        .map((line, index) => {
          const activeClass = index === activeLine ? " active-code-line" : "";
          return `<span class="linked-code-line${activeClass}">${line || "&nbsp;"}</span>`;
        })
        .join("");
    } else {
      const line = lines[activeLine] || "&nbsp;";

      codeBlock.innerHTML = `<span class="linked-code-line active-code-line">${line}</span>`;
    }
  }

  window.refreshTraversalCodeConsole = function () {
    setCode(currentCodeActiveLine);
  };

  window.refreshTraversalCodeConsole = function () {
    setCode(currentCodeActiveLine);
  };

  function clearSvg() {
    while (svg.lastChild && svg.lastChild.tagName !== "defs") {
      svg.removeChild(svg.lastChild);
    }
  }

  function drawList() {
    clearSvg();

    if (nodes.length === 0) {
      addText(350, 165, "Linked list is currently empty", 24, "#6C757D");
      return;
    }

    nodes.forEach((value, index) => {
      const x = 80 + index * 280;
      const y = 125;

      const isActive = index === traverseIndex;

      addNode(x, y, value, isActive);

      if (index < nodes.length - 1) {
        addLine(x + 180, y + 45, x + 280, y + 45);
      } else {
        addText(x + 215, y + 52, "NULL", 20, "#6C757D");
      }
    });

    if (traverseIndex >= 0 && traverseIndex < nodes.length) {
      const pointerX = 80 + traverseIndex * 280 + 90;
      addText(pointerX - 35, 60, "current", 18, "#facc15");
      addPointerLine(pointerX, 90, pointerX, 125);
    }
  }

  function addNode(x, y, value, isActive) {
    const rect = document.createElementNS("http://www.w3.org/2000/svg", "rect");

    rect.setAttribute("x", x);
    rect.setAttribute("y", y);
    rect.setAttribute("width", 180);
    rect.setAttribute("height", 90);
    rect.setAttribute("rx", 18);
    rect.setAttribute("fill", isActive ? "#fff1a8" : "#ffffff");
    rect.setAttribute("stroke", isActive ? "#facc15" : "#202E53");
    rect.setAttribute("stroke-width", isActive ? 7 : 3);
    rect.setAttribute("filter", "url(#linkedSoftShadow)");

    svg.appendChild(rect);

    addText(x + 25, y + 35, "Data: " + value, 21, "#202E53");
    addText(x + 25, y + 65, "Next", 18, "#6B7280");
  }

  function addText(x, y, text, size, fill) {
    const t = document.createElementNS("http://www.w3.org/2000/svg", "text");
    t.setAttribute("x", x);
    t.setAttribute("y", y);
    t.setAttribute("font-size", size);
    t.setAttribute("fill", fill);
    t.setAttribute("font-weight", "700");
    t.textContent = text;
    svg.appendChild(t);
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

  addNodeBtn.addEventListener("click", function () {
    if (nodes.length < nodeValues.length) {
      nodes.push(nodeValues[nodes.length]);
      traverseIndex = -1;

      drawList();
      setCode(0);

      feedback.innerHTML = `<strong>Node added:</strong> Node ${nodes[nodes.length - 1]} was added to the linked list.`;

      if (nodes.length === nodeValues.length) {
        addNodeBtn.disabled = true;
        traverseBtn.disabled = false;
        feedback.innerHTML =
          "All nodes have been added. Now click <strong>Traverse</strong> to move the current pointer through the list.";
      }
    }
  });

  traverseBtn.addEventListener("click", function () {
    if (nodes.length === 0) return;

    traverseIndex++;

    if (traverseIndex >= nodes.length) {
      traverseBtn.disabled = true;
      completeBtn.disabled = false;

      drawList();
      setCode(2);

      feedback.innerHTML =
        "<strong>Traversal complete:</strong> current reached NULL after visiting every node.";

      addText(795, 90, "current = NULL", 22, "#facc15");
      return;
    }

    drawList();

    if (traverseIndex === 0) {
      setCode(0);
    } else {
      setCode(4);
    }

    feedback.innerHTML = `<strong>Traversal:</strong> current is visiting node ${nodes[traverseIndex]}. Then current moves to current->next.`;
  });

  resetListBtn.addEventListener("click", function () {
    nodes = [];
    traverseIndex = -1;

    addNodeBtn.disabled = false;
    traverseBtn.disabled = true;
    completeBtn.disabled = true;

    drawList();
    setCode(0);

    feedback.innerHTML =
      "Click <strong>Add Node</strong> to create the first node in the linked list.";
  });

  drawList();
  setCode(0);
});
