# japanese-word-game

## 프로젝트 소개
Unity를 이용해 일본어를 최대한 쉽고 재미있게 배울 수 있도록 만든 단어 학습 미니 게임입니다.

한글 뜻을 입력하면 일본어 한자를 들고있는 적에게 공격을 가하고 점수를 얻는 게임을 구현 할 예정입니다.

## 제작 목적
-Unity 기초 쌓기

-미니 프로젝트 제작 경험 만들기

## 사용 기술
-Unity

-Unity에 내재된 C#

-폰트

-일본어 한자 외부 데이터 관리

## 진행 상황
#Repository 생성 (2026 02 19 완료)

프로젝트 기획

기본 UI 구성

일본어 단어 랜덤으로 출제 구현

점수 시스템 구현

## 느낀점
2026 02 19 Repositoy를 처음 생성해서 설렌다.

## 개발 기록
### 3/1
- 프로젝트 구조 설계 및 기획

### 3/9
- 일본어 한자 지원을 위해 외부 폰트를 가져와 적용하고 메인 폰트로 지정

- TextMeshPro의 Fallback Font를 활용하여 또 다른 서브폰트를 적용하여 메인 폰트의 미지원 문자 깨짐 문제 해결

### 3/11
- 일본어 단어(test: 食べる)에 대한 사용자의 입력 기반 정답 판별 기능 구현 (InputField 를 통해 엔터 입력 처리 및 결과 출력 구현)

### 3/12
- 사용자 입력으로 정답 입력시 Enemy 오브젝트 제거 로직 구현

- EnemyController을 통한 Enemy 이동 기능 구현

### 3/13 
- Collider2D 및 Rigidbody2D를 활용한 Enemy - Player 충돌 감지 시스템 구현

### 3/15 ~ 3/17
- Enemy Generator를 통해 설정 시간 간격으로 Enemy 스폰 시스템 구현

- EnemyData 클래스를 정의해 Enemy와 단어 데이터(한자, 한글 뜻)를 구조적으로 처리 및 활용

- EnemyListManager를 통한 리스트 기반 EnemyData 데이터 관리 시스템 구현

- foreach 순회를 활용한 정답 매칭 및 해당 First Correct Enemy 오브젝트 제거 로직 구현

- TextMeshPro UI를 활용하여 Enemy 하단에 한자 단어를 표시하는 시각적 요소 구현

### 3/19
- Enemy 제거 이벤트 기반 점수 누적 시스템 구현
  
- Enemy 제거 즉각 피드백 UI 구현

- UI 피드백 로직을 DeathUIManager로 분리하여 추후 확장성 확보

### 3/24
- 플레이어 체력 감소 및 체력바 UI 구현

- 게임 오버 이벤트 구현 (Enemy 전부 삭제, 사용자 입력창 비활성화, Enemy 스폰 중지 기능 등)

## 트러블 슈팅
### 폰트 깨짐 문제
- TextMeshPro의 Fallback Font 기능을 활용하여 여러 폰트를 사용하고 깨지는 폰트를 다른 폰트가 보충할 수 있도록 해결

### Enemy 오브젝트와 문자 데이터 동기화 문제
- EnemyData 클래스를 별도로 정의하여 GameObject와 문자 데이터를 함께 관리

## 추후 개선 사항
- JSON을 활용해 실제 JLPT 단어 호출

- 게임 재미를 위한 Player의 스킬 추가

- Scene을 통해 게임 난이도 세분화
