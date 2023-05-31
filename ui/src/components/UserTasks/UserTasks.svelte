<script lang="ts">
  import { UserTaskDto, UserTasksClient } from "../../nlp-api";
  import AddTask from "./AddTask.svelte";
  import UserTask from "./UserTask.svelte";

  let tasks: UserTaskDto[] = [];

  const refreshTasks = () => {
    tasks = [];
    new UserTasksClient().getAllTasks().then((responseTasks: UserTaskDto[]) => {
      tasks = responseTasks || [];
    });
  }

  const onAddTaskHandler = (task: UserTaskDto) => {
    new UserTasksClient().addTask(task).then((success: boolean) => {
      if(success) refreshTasks();
    });
  };

  refreshTasks();
</script>

<style>
  .wrapper {
    display: flex;
    padding: 2px;
  }
  .add {
    flex: 1;
  }
  .view {
    flex: 3;
  }
</style>

<div class="wrapper">
  <div class="add">
    <AddTask onAddTask={onAddTaskHandler} />
  </div>
  <div class="view">
    {#each tasks as task}
      <UserTask {task} />
    {/each}
  </div>
</div>