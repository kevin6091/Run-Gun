# Run-Gun

좀비가 등장하는 도로를 배경으로 한 세로 화면 슈팅 프로토타입입니다. 플레이어는 터치 입력으로 좌우 이동하며, 무기는 일정 간격으로 자동 발사합니다. 도로 구간이 화면 뒤로 지나가면 전방으로 옮겨 재사용하고, 전방 구간에 적을 배치합니다.

## 플레이 시연

[플레이 영상 GitHub](docs/run-gun-demo.gif) · [플레이 영상 Notion](https://opaque-track-22b.notion.site/_-220ec99d2c54809b846ed7d2c22b53da#420ec99d2c5481fb8657c3cfe7c1b44e)

![도로 스크롤, 좌우 이동, 좀비 등장과 전투 효과가 보이는 Run-Gun 플레이 영상 일부](docs/run-gun-demo.gif)

## 기술 구성

**Unity 6000.1.8f1 · C# · URP 17.1.0 · Cinemachine 3.1.4 · Shader Graph**

| 기능 | 구현 | 코드 |
| --- | --- | --- |
| 다수 드론의 대형 이동 (Boids) | 이웃 드론을 기준으로 분리(Separation)·정렬(Alignment)·응집(Cohesion)을 계산합니다. 공유 목표에 가장 가까운 드론을 1초마다 리더로 고르고, 리더의 목표 추종과 다른 드론의 리더 추종을 가중합에 더해 플레이어 주변에서 간격을 조절하며 함께 이동하도록 구현했습니다. | [DroneController](https://github.com/kevin6091/Run-Gun/blob/e70ff2297999da4659604ffb8b4e22050b30ae25/Assets/Scripts/Controllers/DroneController.cs) · [Drone](https://github.com/kevin6091/Run-Gun/blob/e70ff2297999da4659604ffb8b4e22050b30ae25/Assets/Scripts/Drone/Drone.cs) · `Minjae` 브랜치 |
| 스크롤을 반영한 탄환 판정 | 탄환 첫 갱신에서 탄환 속도 벡터에서 스크롤 속도 벡터를 빼 상대 속도를 구합니다. `Monster` 레이어에 Raycast를 수행하고, 적중 지점까지의 거리를 상대 속력으로 나눈 시간만큼 기다린 뒤 피해와 적중 이펙트를 처리합니다. | [Bullet](Assets/Scripts/Contents/Gun/Bullet.cs) |
| 맵 재사용과 몬스터 스폰 | 스크롤이 진행되면 지나간 번들을 전방으로 옮깁니다. 번들 배치 및 재배치 때 발행한 이벤트를 `MonsterManager`가 받아 마지막 번들 기준으로 기본 몬스터 종류와 등장 수(1~2마리)를 정합니다. | [ScrollSystem](Assets/Scripts/Contents/ScrollSystem.cs) · [BundleLoopSystem](Assets/Scripts/Contents/BundleLoopSystem.cs) · [MonsterManager](Assets/Scripts/Managers/MonsterManager.cs) |
| 오브젝트 풀링 | `ResourceManager`가 `Resources`에서 프리팹을 읽고, `Poolable`이 붙은 오브젝트의 생성과 반환을 스택 기반 풀에 위임합니다. 기본 몬스터 프리팹 3종에 풀이 적용되어 있습니다. | [PoolManager](Assets/Scripts/Managers/PoolManager.cs) · [ResourceManager](Assets/Scripts/Managers/ResoureManager.cs) |
| 총기 데이터와 애니메이션 IK | `GunInfo` ScriptableObject에 총기별 탄환 프리팹, 발사 방향, 초기 속도, 모델의 크기와 위치 보정값을 둡니다. `Gun`은 고정 주기로 발사하고 발사 애니메이션과 파티클을 재생합니다. `OnAnimatorIK`가 총기 소켓에 양손의 위치와 회전을 맞춥니다. | [총기 스크립트와 에셋](Assets/Scripts/Contents/Gun) · [PlayerController](Assets/Scripts/Controllers/PlayerController.cs) |
