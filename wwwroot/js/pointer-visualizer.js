function initializePointerVisualizer() {
  const assignBtn = document.getElementById("assignBtn");
  const dereferenceBtn = document.getElementById("dereferenceBtn");
  const reassignBtn = document.getElementById("reassignBtn");
  const resetBtn = document.getElementById("resetBtn");
  const completeLessonBtn = document.getElementById("completeLessonBtn");

  const pointerValue = document.getElementById("pointerValueSVG");
  const arrowX = document.getElementById("pointerArrow");
  const arrowY = document.getElementById("pointerArrowY");
  const arrowLabel = document.getElementById("arrowLabel");
  const arrowLabelY = document.getElementById("arrowLabelY");
  const dereferencePanel = document.getElementById("dereferencePanel");
  const dereferenceValueSVG = document.getElementById("dereferenceValueSVG");

  const explanation = document.getElementById("stepContent");
  const codeBlock = document.getElementById("pointerCodeBlock");
  let currentCodeActiveLine = 0;

  function animateElement(targets, options) {
    if (window.anime && typeof window.anime.animate === "function") {
      window.anime.animate(targets, options);
    } else if (window.anime && typeof window.anime === "function") {
      window.anime({ targets, ...options });
    }
  }

  function setCode(activeLine) {
    currentCodeActiveLine = activeLine;

    const lines = [
      "int x = 10;",
      "int y = 25;",
      "int* ptr = nullptr;",
      "ptr = &x;",
      "cout << *ptr; // outputs 10",
      "ptr = &y;",
      "cout << *ptr; // outputs 25",
    ];

    const consoleBox = document.getElementById("pointerCodeConsole");
    const isExpanded = consoleBox && consoleBox.classList.contains("expanded");

    if (isExpanded) {
      codeBlock.innerHTML = lines
        .map((line, index) => {
          return `<span class="code-line ${index === activeLine ? "active" : ""}">${line}</span>`;
        })
        .join("");
    } else {
      codeBlock.innerHTML = `<span class="code-line active">${lines[activeLine]}</span>`;
    }
  }

  window.refreshPointerCodeConsole = function () {
    setCode(currentCodeActiveLine);
  };

  function setInitialState() {
    pointerValue.textContent = "NULL";

    arrowX.setAttribute("opacity", "0");
    arrowY.setAttribute("opacity", "0");
    arrowLabel.setAttribute("opacity", "0");
    arrowLabelY.setAttribute("opacity", "0");
    dereferencePanel.setAttribute("opacity", "0");
    dereferenceValueSVG.textContent = "10";

    document.getElementById("xCard").classList.remove("dereference-focus");
    document.getElementById("yCard").classList.remove("dereference-focus");

    // document.getElementById("yCard").setAttribute("opacity", "0");

    dereferenceBtn.disabled = true;
    reassignBtn.disabled = true;
    completeLessonBtn.disabled = true;

    setCode(0);

    explanation.innerHTML =
      "<strong>Step 1:</strong> Variable <strong>x</strong> is created with value <strong>10</strong> at memory address <strong>1001</strong>.";
  }

  assignBtn.addEventListener("click", () => {
    pointerValue.textContent = "1001";
    arrowY.setAttribute("opacity", "0");
    arrowLabelY.setAttribute("opacity", "0");

    arrowX.setAttribute("opacity", "1");
    arrowLabel.setAttribute("opacity", "1");

    document.getElementById("yCard").classList.remove("dereference-focus");
    document.getElementById("xCard").classList.remove("dereference-focus");

    dereferencePanel.setAttribute("opacity", "0");
    dereferenceValueSVG.textContent = "10";

    setCode(3);

    explanation.innerHTML =
      "<strong>Step 2:</strong> The pointer <strong>ptr</strong> now stores the address of <strong>x</strong>. Since x is stored at <strong>1001</strong>, ptr stores <strong>1001</strong>.";

    arrowX.setAttribute("opacity", "1");
    arrowLabel.setAttribute("opacity", "1");

    animateElement("#ptrCard", {
      translateY: [-8, 0],
      duration: 500,
      ease: "outElastic(1, .7)",
    });

    animateElement("#xCard", {
      scale: [1, 1.05, 1],
      duration: 700,
      ease: "outElastic(1, .6)",
    });

    animateElement("#pointerArrow", {
      opacity: [0, 1],
      strokeDashoffset: [260, 0],
      duration: 900,
      ease: "outExpo",
    });

    animateElement("#arrowLabel", {
      opacity: [0, 1],
      translateY: [-8, 0],
      duration: 600,
      ease: "outExpo",
    });

    dereferenceBtn.disabled = false;
    dereferenceValueSVG.textContent = "10";
    dereferencePanel.setAttribute("opacity", "0");
  });

  dereferenceBtn.addEventListener("click", () => {
    const ptrValue = pointerValue.textContent;

    if (ptrValue === "1001") {
      setCode(4);
      dereferenceValueSVG.textContent = "10";

      explanation.innerHTML =
        "<strong>Dereference:</strong> <strong>*ptr</strong> follows address <strong>1001</strong> and reads the value stored inside <strong>x</strong>, which is <strong>10</strong>.";

      document.getElementById("xCard").classList.add("dereference-focus");
      document.getElementById("yCard").classList.remove("dereference-focus");

      animateElement("#xCard", {
        scale: [1, 1.12, 1],
        duration: 850,
        ease: "outElastic(1, .6)",
      });
    }

    if (ptrValue === "3001") {
      setCode(6);
      dereferenceValueSVG.textContent = "25";

      explanation.innerHTML =
        "<strong>Dereference:</strong> <strong>*ptr</strong> now follows address <strong>3001</strong> and reads the value stored inside <strong>y</strong>, which is <strong>25</strong>.";

      document.getElementById("yCard").classList.add("dereference-focus");
      document.getElementById("xCard").classList.remove("dereference-focus");

      animateElement("#yCard", {
        scale: [1, 1.12, 1],
        duration: 850,
        ease: "outElastic(1, .6)",
      });
    }

    dereferencePanel.setAttribute("opacity", "1");

    animateElement("#dereferencePanel", {
      opacity: [0, 1],
      translateY: [-10, 0],
      scale: [0.95, 1],
      duration: 650,
      ease: "outExpo",
    });

    reassignBtn.disabled = false;
  });

  reassignBtn.addEventListener("click", () => {
    pointerValue.textContent = "3001";
    setCode(5);

    explanation.innerHTML =
      "<strong>Step 4:</strong> A pointer can be reassigned. Now <strong>ptr</strong> stores the address of <strong>y</strong>, which is <strong>3001</strong>. The pointer no longer points to x.";

    arrowX.setAttribute("opacity", "0");
    arrowLabel.setAttribute("opacity", "0");

    document.getElementById("yCard").setAttribute("opacity", "1");
    arrowY.setAttribute("opacity", "1");
    arrowLabelY.setAttribute("opacity", "1");

    animateElement("#yCard", {
      opacity: [0, 1],
      translateY: [25, 0],
      scale: [0.96, 1],
      duration: 700,
      ease: "outExpo",
    });

    animateElement("#ptrCard", {
      scale: [1, 1.05, 1],
      duration: 650,
      ease: "outElastic(1, .6)",
    });

    animateElement("#pointerArrowY", {
      opacity: [0, 1],
      strokeDashoffset: [120, 0],
      duration: 850,
      ease: "outExpo",
    });

    animateElement("#arrowLabelY", {
      opacity: [0, 1],
      translateY: [-8, 0],
      duration: 600,
      ease: "outExpo",
    });

    completeLessonBtn.disabled = false;
    dereferenceBtn.disabled = false;
    dereferenceValueSVG.textContent = "25";
    dereferencePanel.setAttribute("opacity", "0");

    document.getElementById("xCard").classList.remove("dereference-focus");
    document.getElementById("yCard").classList.remove("dereference-focus");
  });

  resetBtn.addEventListener("click", () => {
    setInitialState();

    animateElement(["#xCard", "#ptrCard"], {
      scale: [1.03, 1],
      duration: 400,
      ease: "inOutQuad",
    });
  });

  setInitialState();
}

document.addEventListener("DOMContentLoaded", initializePointerVisualizer);
