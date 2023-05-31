<script lang="ts">
  import { UserTaskDto } from "../../nlp-api";
  export let onAddTask: (task: UserTaskDto) => void;

  let task = new UserTaskDto();
  let taskValid = false;

  const validateTask = () => {
    taskValid = false;
    if(task?.taskName?.length < 3) return;
    if(task?.taskCategory?.length < 3) return;
    if(!task?.taskPriority) return;
    taskValid = true;
  }

  const createTask = () => {
    onAddTask(task);
    task = new UserTaskDto();
    taskValid = false;
  }
</script>

<div class="add-task">
  <h2>Add Task</h2>

  <table>
    <tr>
      <td>Name</td>
      <td>
        <input type="text" placeholder="Task Name" bind:value={task.taskName} on:keyup={validateTask}>
      </td>
    </tr>
    <tr>
      <td>Category</td>
      <td>
        <input type="text" placeholder="Task Category" bind:value={task.taskCategory} on:keyup={validateTask}>
      </td>
    </tr>
    <tr>
      <td>Priority</td>
      <td>
        <input type="number" min="1" max="256" bind:value={task.taskPriority} on:keyup={validateTask} on:change={validateTask}>
      </td>
    </tr>
    <tr>
      <td>Description</td>
      <td>
        <textarea cols="30" rows="10" bind:value={task.taskDescription} on:keyup={validateTask}></textarea>
      </td>
    </tr>
    <tr>
      <td colspan="2">
        <button type="button" disabled={!taskValid} on:click={createTask}>Add Task</button>
      </td>
    </tr>
  </table>
</div>