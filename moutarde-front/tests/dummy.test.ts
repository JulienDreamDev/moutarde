import { test, expect } from "vitest";

const dummy = () => true;

test("dummy function returns true", () => {
  expect(dummy()).toBe(true);
});
